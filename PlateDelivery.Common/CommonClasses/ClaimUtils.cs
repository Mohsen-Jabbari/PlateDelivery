﻿using System.Security.Claims;
using System.Security.Principal;

namespace PlateDelivery.Common.CommonClasses;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    public static string GetAvatar(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirst(ClaimTypes.UserData)?.Value.ToString();
    }
}