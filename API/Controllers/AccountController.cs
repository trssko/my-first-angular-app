using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(AppDbContext context) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
    {
        if (await EmailUsed(registerDTO.Mail)) return BadRequest("Email is already in use.");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDTO.DisplayName,
            Mail = registerDTO.Mail,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Mail)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
    
    private async Task<bool> EmailUsed(string mail)
    {
        return await context.Users.AnyAsync(x => x.Mail.ToLower() == mail.ToLower());
    }
}
