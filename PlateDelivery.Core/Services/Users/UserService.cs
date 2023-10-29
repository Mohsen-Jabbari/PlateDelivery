using Newtonsoft.Json.Linq;
using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.UserAgg;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AddToken(UserToken token, long UserId)
    {
        var user = _userRepository.GetTracking(UserId);
        if (user.Result == null)
            return null;
        user.Result.AddToken(token.HashJwtToken, token.HashRefreshToken, token.TokenExpireDate,
            token.RefreshTokenExpireDate, token.Device);
        await _userRepository.Save();
        return user.Result;
    }

    public async Task<bool> AddToken(long UserId, string hashToken, string hashRefreshToken, DateTime expireTokenDate, DateTime expireRefreshTokenDate, string device)
    {
        var user = await _userRepository.GetTracking(UserId);
        if (user == null)
            return false;
        user.AddToken(hashToken, hashRefreshToken, expireTokenDate,
        expireRefreshTokenDate, device);
        await _userRepository.Save();
        return true;
    }

    public async Task<long> CreateUser(string UserName, string Password)
    {
        if (!await _userRepository.ExistsAsync(u => u.UserName == UserName))
        {
            var user = new User(UserName, Password, true, false);
            _userRepository.Add(user);
            await _userRepository.Save();
            return user.Id;
        }
        return -1;
    }

    public async Task<bool> DeleteUser(long UserId)
    {
        var user = await _userRepository.GetAsync(UserId);
        if (user != null)
        {
            user.SetIsDeleteTrue();
            await _userRepository.Save();
            return true;
        }
        return false;
    }

    public async Task<User> GetUserById(long UserId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetUserTokenByJwtToken(string JwtToken)
    {
        throw new NotImplementedException();
    }

    public UserDto Login(string UserName)
    {
        var user = _userRepository.GetUserByUserName(UserName);
        if (user != null)
            return new UserDto()
            {
                CreationDate = user.CreationDate,
                Id = user.Id,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete,
                Password = user.Password,
                UserName = user.UserName
            };
        return null;
    }

    public async Task<bool> SetActive(long UserId)
    {
        var user = await _userRepository.GetAsync(UserId);
        if (user != null)
        {
            user.SetActive(true);
            await _userRepository.Save();
            return true;
        }
        return false;
    }
}