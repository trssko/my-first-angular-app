using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Extensions;

public static class AppUserExtension
{
    public static UserDTO toDto(this AppUser user, ITokenService tokenService) {
        return new UserDTO{
            DisplayName = user.DisplayName,
            Email = user.Mail,
            Id = user.Id,
            Token = tokenService.CreateToken(user)
        };
    }
}
