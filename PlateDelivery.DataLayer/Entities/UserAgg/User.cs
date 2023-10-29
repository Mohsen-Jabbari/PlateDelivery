using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;

namespace PlateDelivery.DataLayer.Entities.UserAgg;
public class User : BaseEntity
{
    public User(string userName, string password, bool isActive, bool isDelete)
    {
        Guard(userName, password);
        UserName = userName;
        Password = password;
        IsActive = isActive;
        IsDelete = isDelete;
    }

    public void ResetPassword(string password)
    {
        Password = password;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetInActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetIsDeleteTrue()
    {
        IsDelete = true;
    }

    public void SetIsDeleteFalse()
    {
        IsDelete = false;
    }

    #region Token

    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
        if (activeTokenCount == 3)
            throw new InvalidDataException("امکان استفاده از 4 سیستم همزمان وجود ندارد");
        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device)
        {
            UserId = Id
        };
        Tokens.Add(token);
    }

    public string RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
        if (token == null)
            throw new InvalidDataException("invalid TokenId");

        Tokens.Remove(token);
        return token.HashJwtToken;
    }

    #endregion

    public static void Guard(string userName, string password)
    {
        NullOrEmptyDataException.CheckString(userName, nameof(userName));
        NullOrEmptyDataException.CheckString(password, nameof(password));
    }

    public string UserName { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDelete { get; private set; }
    public List<UserToken> Tokens { get; private set; }
}
