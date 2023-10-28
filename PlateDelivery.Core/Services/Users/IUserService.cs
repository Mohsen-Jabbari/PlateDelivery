using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Services.Users;
public interface IUserService
{
    Task<long> CreateUser(string UserName, string Password);
    Task<bool> DeleteUser(long UserId);
    Task<bool> SetActive(long UserId);
    Task<string> GetUserTokenByJwtToken(string JwtToken);

    Task<User> GetUserById(long UserId);
}
