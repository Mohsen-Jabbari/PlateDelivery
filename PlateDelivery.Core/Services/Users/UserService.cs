using Azure.Core;
using Dapper;
using Newtonsoft.Json.Linq;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Security;
using PlateDelivery.DataLayer.DapperContext;
using PlateDelivery.DataLayer.Entities.UserAgg;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly DapperContext _dapperContext;

    public UserService(IUserRepository userRepository, DapperContext dapperContext)
    {
        _userRepository = userRepository;
        _dapperContext = dapperContext;
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

    public async Task<bool> LogOut(string JwtToken)
    {
        var hashJwtToken = Sha256Hasher.Hash(JwtToken);
        var tokenDto = await GetUserTokenByJwtToken(hashJwtToken);
        if (tokenDto == null)
            return false;

        var user = await _userRepository.GetTracking(tokenDto.UserId);
        if (user == null)
            return false;

        var token = user.Tokens.Where(t => t.Id == tokenDto.Id).FirstOrDefault();
        user.Tokens.Remove(token);
        await _userRepository.Save();
        return true;
    }

    public async Task<UserTokenDto?> GetTokenDtoAsync(string HashJwtToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.Tokens} Where HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto?>(sql, new { hashJwtToken = HashJwtToken });
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

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string hashJwtToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {_dapperContext.Tokens} Where HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto?>(sql, new { hashJwtToken = hashJwtToken });
    }
}