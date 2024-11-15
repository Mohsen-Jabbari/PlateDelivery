﻿using Microsoft.IdentityModel.Tokens;
using PlateDelivery.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlateDelivery.Web.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(LoginViewModel user, IConfiguration configuration)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(12),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}