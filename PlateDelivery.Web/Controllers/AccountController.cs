using Employment.API.Infrastructure.JwtUtil;
using LatinMedia.Core.Convertors;
using LatinMedia.Core.Genertors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models;
using PlateDelivery.Core.Security;
using PlateDelivery.Core.Services.Users;
using PlateDelivery.DataLayer.Context;
using PlateDelivery.DataLayer.Entities.UserAgg;
using PlateDelivery.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UAParser;

namespace PlateDelivery.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRender;
        private readonly PlateDeliveryDBContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IViewRenderService viewRender,
            PlateDeliveryDBContext context, IConfiguration configuration)
        {
            _userService = userService;
            _viewRender = viewRender;
            _context = context;
            _configuration = configuration;
        }

        [Route("Login")]
        public IActionResult Login(bool EditProfile = false, bool permission = true, bool ChangePassword = false)
        {
            ViewBag.EditProfile = EditProfile;
            ViewBag.Permission = permission;
            ViewBag.ChangePassword = ChangePassword;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            if (!Captcha.ValidateCaptchaCode(FixedText.FixedEmail(login.CaptchaCode), HttpContext))
            {
                ModelState.AddModelError("CaptchaCode", "کد وارد شده صحیح نمی باشد .");
                return View(login);
            }
            login.UserName = FixedText.FixedMobile(login.UserName);
            login.UnHashedPassword = FixedText.FixedTxt(login.Password);
            login.Password = Sha256Hasher.Hash(FixedText.FixedTxt(login.Password));
            login.CaptchaCode = FixedText.FixedEmail(login.CaptchaCode);
            var user = _userService.Login(login.UserName);
            if (Sha256Hasher.IsCompare(user.Password, login.UnHashedPassword) == false)
            {
                ModelState.AddModelError("Password", "کاربری با مشخصات وارد شده یافت نشد");
                return View(login);
            }

            if (user.IsActive == false)
            {
                ModelState.AddModelError("UserName", "حساب کاربری شما غیرفعال است");
                return View(login);
            }

            login.UserId = user.Id;
            var tokenString = AddTokenAndGenerateJwt(login);
            if(tokenString.Id > 0)
            {
                var token = tokenString.Token;
                var refreshToken = tokenString.RefreshToken;
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = tokenString.ExpireDate
                });
                HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = tokenString.ExpireDate.AddHours(12)
                }); ;
                SyncUser(tokenString.Token);
                ViewBag.IsSuccess = true;
                return View();
            }

            else if(tokenString.Id == -1)
            {
                ViewBag.IsSuccess = false;
                ViewBag.Failure = tokenString.Token;
                return View();
            }
            return View(login);
        }
        private UserTokenResponseDto AddTokenAndGenerateJwt(LoginViewModel user)
        {
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers["user-agent"].ToString();
            var device = "windows";
            if (header != null)
            {
                var info = uaParser.Parse(header);
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            var refreshToken = Guid.NewGuid().ToString();

            var hashJwt = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            var tokenGenerate = new UserToken(hashJwt,refreshToken, DateTime.Now.AddHours(12), DateTime.Now.AddHours(24), device);

            try
            {
                var tokenResult = _userService.AddToken(tokenGenerate, user.UserId);
                if (tokenResult == null)
                    return null;

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                return new UserTokenResponseDto()
                {
                    Token = token,
                    ExpireDate = DateTime.Now.AddHours(12),
                    UserId = jwt.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value,
                    RefreshToken = refreshToken,
                    CreationDate = tokenResult.Result.CreationDate,
                    Id = tokenResult.Result.Id
                };
            }
            
            
            catch (Exception ex)
            {
                return new UserTokenResponseDto()
                {
                    Token = ex.InnerException?.Message,
                    ExpireDate = DateTime.MinValue,
                    UserId = "0",
                    RefreshToken = "",
                    CreationDate = DateTime.MinValue,
                    Id = -1,
                };
            }

            
        }
        private void SyncUser(string token)
        {
            HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                ViewBag.Token = false;
                return Redirect("/Login");
            }
            var result = await _userService.LogOut(token);

            if (result)
            {
                HttpContext.Response.Cookies.Delete("token");
                HttpContext.Response.Cookies.Delete("refresh-token");
            }
            ViewBag.Token = true;
            return Redirect("/Login");
        }
    }
}