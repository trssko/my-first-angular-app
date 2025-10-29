using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if (await EmailUsed(registerDTO.Mail)) return BadRequest("Email is already in use.");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDTO.DisplayName,
            Mail = registerDTO.Mail,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user.toDto(tokenService);
    }

    [HttpPost("login")] // api/account/login
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO) {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Mail == loginDTO.Email);

        if (user == null) return Unauthorized("Invalid email address");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (var i = 0; i < computedHash.Length; i++) {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        return user.toDto(tokenService);
    }
    
    private async Task<bool> EmailUsed(string mail)
    {
        return await context.Users.AnyAsync(x => x.Mail.ToLower() == mail.ToLower());
    }
}
