using Dapper;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Models.Users;
using PlateDelivery.Core.Security;
using PlateDelivery.DataLayer.DapperContext;
using PlateDelivery.DataLayer.Entities.RoleAgg.Repository;
using PlateDelivery.DataLayer.Entities.UserAgg;
using PlateDelivery.DataLayer.Entities.UserAgg.Repository;

namespace PlateDelivery.Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly DapperContext _dapperContext;

    public UserService(IUserRepository userRepository, DapperContext dapperContext, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _dapperContext = dapperContext;
        _roleRepository = roleRepository;
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

    public long CreateUser(CreateUserViewModel model)
    {
        if (!_userRepository.Exists(u => u.UserName == model.UserName))
        {
            var Password = Sha256Hasher.Hash(model.Password);
            var user = new User(model.FirstName, model.LastName,
                model.UserName, Password, model.IsActive, false);
            _userRepository.Add(user);
            _userRepository.SaveSync();
            return user.Id;
        }
        return -1;
    }

    public bool DeleteUser(long UserId)
    {
        var user = _userRepository.GetTrackingSync(UserId);
        if (user != null)
        {
            user.SetIsDeleteTrue();
            _userRepository.SaveSync();
            return true;
        }
        return false;
    }

    public bool RestoreUser(long UserId)
    {
        var user = _userRepository.GetTrackingSync(UserId);
        if (user != null)
        {
            user.SetIsDeleteFalse();
            _userRepository.SaveSync();
            return true;
        }
        return false;
    }

    public async Task<User> GetUserById(long UserId) => await _userRepository.GetTracking(UserId);

    public EditUserViewModel GetUserByIdForEdit(long UserId)
    {
        var user = _userRepository.GetTrackingSync(UserId);
        if (user != null)
            return new EditUserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreationDate = user.CreationDate,
                Id = user.Id,
                IsActive = user.IsActive,
                UserRoles = user.UserRoles.Select(u => u.RoleId).ToList(),
            };
        return null;
    }

    public bool IsUserNameExitsts(string UserName)
    {
        return _userRepository.Exists(u => u.UserName == UserName);
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

    //public List<UserTokenDto>? GetExpiredToken()
    //{
    //    using var connection = _dapperContext.CreateConnection();
    //    var sql = $"SELECT * FROM {_dapperContext.Tokens} Where RefreshTokenExpireDate < @dateNow";
    //    var result = connection.Query<UserTokenDto?>(sql, new { dateNow = DateTime.Now });
    //    return result.ToList();
    //}

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

    #region SideBar
    public async Task<SideBarAdminPanelViewModel> GetSideBarAdminPanelData(long UserId)
    {
        var user = await _userRepository.GetTracking(UserId);
        var userRoles = new List<UserRoleDto>();
        userRoles.Add(new UserRoleDto() { UR_Id = 1, UserId = 20, RoleId = 1 });
        var result = new SideBarAdminPanelViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            AvatarName = "defualt.png",
            Gender = "",
            RollId = userRoles
        };
        return result;
        //.Select(u => new SideBarAdminPanelViewModel()
        //{
        //    FirstName = PasswordHelper.DecryptString(u.FirstName),
        //    LastName = PasswordHelper.DecryptString(u.LastName),
        //    AvatarName = u.UserAvatar,
        //    Gender = u.Gender.GenderName,
        //    RollId = u.UserRoles
        //}).Single();
    }

    #endregion

    #region Users

    public InformationUserViewModel GetUserInformation(long UserId)
    {
        var user = _userRepository.GetTrackingSync(UserId);
        if (user == null)
            return null;
        return new InformationUserViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreationDate = user.CreationDate,
            Id = user.Id,
            UserName = user.UserName
        };
    }
    public UsersViewModel GetUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "")
    {
        var result = _userRepository.GetAll();  //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByLastName))
            {
                result = result.Where(u => u.LastName.Contains(filterByLastName) || u.FirstName.Contains(filterByLastName)).ToList();
            }

            if (!string.IsNullOrEmpty(filterByUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterByUserName)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            UsersViewModel list = new UsersViewModel();
            list.Users = result.Where(u => u.IsDelete == false).OrderByDescending(u => u.CreationDate).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.LastPage = list.PageCount;
            list.PrevPage = Math.Max(pageId - 1, list.CurrentPage);
            list.NextPage = Math.Max(pageId + 1, list.LastPage);
            list.UserCounts = result.Count;
            return list;
        }
        return new UsersViewModel();
    }


    public UsersViewModel GetDeleteUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "")
    {
        var result = _userRepository.GetAll();  //lazyLoad;

        if (result != null)
        {
            if (!string.IsNullOrEmpty(filterByLastName))
            {
                result = result.Where(u => u.LastName.Contains(filterByLastName) || u.FirstName.Contains(filterByLastName)).ToList();
            }

            if (!string.IsNullOrEmpty(filterByUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterByUserName)).ToList();
            }

            int takeData = take;
            int skip = (pageId - 1) * takeData;

            UsersViewModel list = new UsersViewModel();
            list.Users = result.Where(u => u.IsDelete == true).OrderByDescending(u => u.CreationDate).Skip(skip).Take(takeData).ToList();
            list.PageCount = (int)Math.Ceiling(result.Count / (double)takeData);
            list.CurrentPage = pageId;
            list.LastPage = list.PageCount;
            list.PrevPage = Math.Max(pageId - 1, list.CurrentPage);
            list.NextPage = Math.Max(pageId + 1, list.LastPage);
            list.UserCounts = result.Count;
            return list;
        }
        return new UsersViewModel();
    }

    public bool EditUser(EditUserViewModel model)
    {
        var oldUser = _userRepository.GetTrackingSync(model.Id);
        if (oldUser != null)
        {
            oldUser.EditUser(model.FirstName, model.LastName, model.IsActive);
            _userRepository.SaveSync();
            return true;
        }
        return false;
    }

    public bool ChangePasswordViewModel(long UserId, string Password)
    {
        var user = _userRepository.GetTrackingSync(UserId);
        if (user == null)
            return false;
        var HashPassword = Sha256Hasher.Hash(Password);
        user.ResetPassword(HashPassword);
        _userRepository.SaveSync();
        return true;
    }

    #endregion



    #region UserRole

    public bool AddRolesToUser(List<long> roleIds, long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user == null)
            return false;
        List<UserRole> roles = new();
        foreach (var roleId in roleIds)
        {
            roles.Add(new UserRole(userId, roleId));
        }
        user.SetRoles(roles);
        _userRepository.SaveSync();
        return true;
    }
    public bool EditRolesUser(List<long> rolesId, long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user == null)
            return false;
        List<UserRole> roles = new();
        foreach (var roleId in rolesId)
        {
            roles.Add(new UserRole(userId, roleId));
        }
        user.SetRoles(roles);
        _userRepository.SaveSync();
        return true;
    }
    public async Task<bool> RemoveUserRoles(long userId)
    {
        var user = await _userRepository.GetTracking(userId);
        if (user == null)
            return false;
        user.UserRoles.Clear();
        await _userRepository.Save();
        return true;
    }


    public long GetUserRoleById(long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user == null)
            return -1;
        var userRole = user.UserRoles.Where(u => u.UserId == userId).FirstOrDefault();
        if (userRole != null)
            return userRole.Id;
        return -1;
    }

    public List<string> GetUserRoles(long userId)
    {
        var user = _userRepository.GetTrackingSync(userId);
        if (user == null)
            return new List<string>();
        if (user.UserRoles.Any())
        {
            var rolesId = user.UserRoles.Select(u => u.RoleId).ToArray();
            var roles = _roleRepository.GetAll();
            if (roles == null)
                return new List<string>();
            else
            {
                var result = roles.Where(r => rolesId.Contains(r.Id)).Select(r => r.RoleName).ToList();
                return result;
            }
        }
        return new List<string>();
    }

    public void RemoveExpiredTokens()
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"DELETE FROM {_dapperContext.Tokens} Where RefreshTokenExpireDate < @dateNow";
        connection.QueryFirstOrDefault<UserTokenDto?>(sql, new { dateNow = DateTime.Now });
    }

    #endregion

}