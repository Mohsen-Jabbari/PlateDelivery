using Microsoft.AspNetCore.Authentication.JwtBearer;
using PlateDelivery.Common.CommonClasses;
using PlateDelivery.Core.Services.Users;

namespace PlateDelivery.Web.JwtUtil;

public class CustomJwtValidation
{
    private readonly IUserService _userService;

    public CustomJwtValidation(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Validate(TokenValidatedContext context)
    {
        var userId = context.Principal.GetUserId();
        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var token = await _userService.GetUserTokenByJwtToken(jwtToken);
        if (token == null)
        {
            context.Fail("Token NotFound");
            return;
        }

        var user = await _userService.GetUserById(userId);
        if (user == null || user.IsActive == false)
        {
            context.Fail("User InActive");
            return;
        }
    }
}