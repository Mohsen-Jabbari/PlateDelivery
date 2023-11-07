using Dapper;
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

    public async Task<long> CreateUser(string FirstName, string LastName, string UserName, string Password)
    {
        if (!await _userRepository.ExistsAsync(u => u.UserName == UserName))
        {
            var user = new User(FirstName, LastName, UserName, Password, true, false);
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

    public async Task<User> GetUserById(long UserId) => await _userRepository.GetTracking(UserId);

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

    public UsersViewModel GetUsers(int pageId = 1, int take = 10, string? filterByLastName = "", string? filterByUserName = "")
    {
        var result = _userRepository.GetAll();  //lazyLoad;

        if(result != null)
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
            list.Users = result.OrderByDescending(u => u.CreationDate).Skip(skip).Take(takeData).ToList();
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

    public Task<long> EditUser(long Id, string FirstName, string LastName, string Password)
    {
        throw new NotImplementedException();
    }

    #endregion

}