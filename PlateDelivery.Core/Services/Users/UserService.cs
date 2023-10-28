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