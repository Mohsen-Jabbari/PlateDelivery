﻿using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Services.Users;
public interface IUserService
{
    Task<long> CreateUser(string UserName, string Password);
    Task<bool> DeleteUser(long UserId);
    Task<bool> SetActive(long UserId);
    Task<string> GetUserTokenByJwtToken(string JwtToken);

    Task<User> GetUserById(long UserId);
    UserDto Login(string UserName);
    Task<User> AddToken(UserToken token, long UserId);
    Task<bool> AddToken(long UserId, string hashToken, string hashRefreshToken, DateTime expireTokenDate, DateTime expireRefreshTokenDate, string device);
}