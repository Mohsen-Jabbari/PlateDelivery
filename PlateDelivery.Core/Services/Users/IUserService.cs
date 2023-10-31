using PlateDelivery.Core.Models;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Services.Users;
public interface IUserService
{
    Task<long> CreateUser(string FirstName, string LastName, string UserName, string Password);
    Task<long> EditUser(long Id, string FirstName, string LastName, string Password);
    Task<bool> DeleteUser(long UserId);
    Task<bool> SetActive(long UserId);
    Task<bool> LogOut(string JwtToken);

    Task<User> GetUserById(long UserId);
    UserDto Login(string UserName);
    Task<User> AddToken(UserToken token, long UserId);
    Task<UserTokenDto?> GetUserTokenByJwtToken(string JwtToken);

    #region SideBar
    SideBarAdminPanelViewModel GetSideBarAdminPanelData(long UserId);
    #endregion

    #region Users

    UsersViewModel GetUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "");

    #endregion
}
