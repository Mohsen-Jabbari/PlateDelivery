using PlateDelivery.Core.Models;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.DataLayer.Entities.UserAgg;

namespace PlateDelivery.Core.Services.Users;
public interface IUserService
{
    long CreateUser(CreateUserViewModel model);
    bool EditUser(EditUserViewModel model);
    bool DeleteUser(long UserId);
    bool RestoreUser(long UserId);
    Task<bool> SetActive(long UserId);
    Task<bool> LogOut(string JwtToken);
    bool IsUserNameExitsts(string UserName);

    Task<User> GetUserById(long UserId);
    EditUserViewModel GetUserByIdForEdit(long UserId);
    UserDto Login(string UserName);
    Task<User> AddToken(UserToken token, long UserId);
    Task<UserTokenDto?> GetUserTokenByJwtToken(string JwtToken);

    #region SideBar
    Task<SideBarAdminPanelViewModel> GetSideBarAdminPanelData(long UserId);
    #endregion

    #region Users

    InformationUserViewModel GetUserInformation(long UserId);
    UsersViewModel GetUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "");
    UsersViewModel GetDeleteUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "");
    bool ChangePasswordViewModel(long UserId, string Password);

    #endregion

    #region UserRole

    bool AddRolesToUser(List<long> roleIds, long userId);
    bool EditRolesUser(List<long> rolesId, long userId);
    Task<bool> RemoveUserRoles(long userId);

    long GetUserRoleById(long userId);
    List<string> GetUserRoles(long userId);

    #endregion
}
