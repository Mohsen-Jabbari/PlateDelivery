using LatinMedia.Core.Genertors;
using Microsoft.AspNetCore.Mvc;
using PlateDelivery.Core.Services.Users;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace LatinMedia.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IHostingEnvironment environment;

        public HomeController(IUserService userService, IHostingEnvironment environment)
        {
            _userService = userService;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View("index");
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        //[Route("OnlinePayment/{id}")]
        //public IActionResult OnlinePayment(int id)
        //{
        //    if (HttpContext.Request.Query["Status"].ToString() != ""
        //       && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
        //       && HttpContext.Request.Query["Authority"] != "")
        //    {
        //        string authority = HttpContext.Request.Query["Authority"];
        //        var wallet = _userService.GetWalletByWalletId(id);
        //        var payment = new ZarinpalSandbox.Payment(wallet.Amount);
        //        var response = payment.Verification(authority).Result;
        //        if (response.Status == 100) // pay is success
        //        {
        //            ViewBag.Code = response.RefId;
        //            ViewBag.IsSuccess = true;
        //            wallet.IsPay = true;
        //            _userService.UpdateWallet(wallet);
        //        }
        //    }
        //    return View();
        //}

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                environment.WebRootPath, "upload",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/upload/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
    }
}