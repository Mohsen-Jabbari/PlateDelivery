﻿using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.DataLayer.Entities.UserAgg;
public class UserToken : BaseEntity
{
    private UserToken()
    {

    }


    public UserToken(string hashJwtToken, string hashRefreshToken, 
        DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        Guard(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }

    public long UserId { get; internal set; }
    public string HashJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }

    public void Guard(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        NullOrEmptyDataException.CheckString(hashJwtToken, nameof(HashJwtToken));
        NullOrEmptyDataException.CheckString(hashRefreshToken, nameof(HashRefreshToken));

        if (tokenExpireDate < DateTime.Now)
            throw new InvalidDataException("Invalid Token ExpireDate");

        if (refreshTokenExpireDate < tokenExpireDate)
            throw new InvalidDataException("Invalid RefreshToken ExpireDate");
    }
}
