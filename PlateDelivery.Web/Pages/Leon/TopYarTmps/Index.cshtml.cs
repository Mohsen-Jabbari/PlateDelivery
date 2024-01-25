using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Accounts;
using PlateDelivery.Core.Services.ServiceCodings;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[PermissionChecker(29)]
    public class IndexModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;
        private readonly IAccountService _accountService;
        private readonly IServiceCodingService _serviceCodingService;

        public IndexModel(ITopYarTmpService topYarTmpService, IAccountService accountService, IServiceCodingService serviceCodingService)
        {
            _topYarTmpService = topYarTmpService;
            _accountService = accountService;
            _serviceCodingService = serviceCodingService;
        }

        public TopYarTmpViewModel TopYarTmpViewModel { get; set; }
        public List<CreateAndEditServiceCodeingViewModel> UnregisteredServices;
        public void OnGet(int pageId = 1, int take = 50, string? filterByRRN = "", string? filterByTrackingNo = "", string? filterByTransactionDate = "",
                    string? filterByIban = "", string? filterByAmount = "", string? filterByTerminal = "", string? filterByServiceCode = "",
                       string? filterByProvinceName = "", string? filterBySubProvince = "")
        {
            ViewData["Title"] = "دیتای تاپ یار";

            if (Request.Query.ContainsKey("frrn"))
            {
                string? s = Request.Query["frrn"];
                if (s != "")
                    filterByRRN = s;
            }

            if (Request.Query.ContainsKey("ftn"))
            {
                string? s = Request.Query["ftn"];
                if (s != "")
                    filterByTrackingNo = Request.Query["ftn"];
            }

            if (Request.Query.ContainsKey("ftd"))
            {
                string? s = Request.Query["ftd"];
                if (s != "")
                    filterByTransactionDate = s;
            }

            if (Request.Query.ContainsKey("fiban"))
            {
                string? s = Request.Query["fiban"];
                if (s != "")
                    filterByIban = s;
            }

            if (Request.Query.ContainsKey("famnt"))
            {
                string? s = Request.Query["famnt"];
                if (s != "")
                    filterByAmount = s;
            }

            if (Request.Query.ContainsKey("ftrmnl"))
            {
                string? s = Request.Query["ftrmnl"];
                if (s != "")
                    filterByTerminal = s;
            }

            if (Request.Query.ContainsKey("fsc"))
            {
                string? s = Request.Query["fsc"];
                if (s != "")
                    filterByServiceCode = s;
            }

            if (Request.Query.ContainsKey("fpn"))
            {
                string? s = Request.Query["fpn"];
                if (s != "")
                    filterByProvinceName = s;
            }

            if (Request.Query.ContainsKey("fsp"))
            {
                string? s = Request.Query["fsp"];
                if (s != "")
                    filterBySubProvince = s;
            }

            ViewData["filterRRN"] = filterByRRN;
            ViewData["filterTrackingNo"] = filterByTrackingNo;
            ViewData["filterTransactionDate"] = filterByTransactionDate;
            ViewData["filterIban"] = filterByIban;
            ViewData["filterAmount"] = filterByAmount;
            ViewData["filterTerminal"] = filterByTerminal;
            ViewData["filterServiceCode"] = filterByServiceCode;
            ViewData["filterProvinceName"] = filterByProvinceName;
            ViewData["filterSubProvince"] = filterBySubProvince;

            ViewData["PageID"] = (pageId - 1) * take + 1;
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps(pageId, take, filterByRRN, filterByTrackingNo, filterByTransactionDate,
                    filterByIban, filterByAmount, filterByTerminal, filterByServiceCode,
                       filterByProvinceName, filterBySubProvince);

            //پیدا کردن شماره شبا های نامرتبط با موسسه
            if (TopYarTmpViewModel.ProvinceMessage == null)
            {
                var services = _serviceCodingService.GetServiceCodings(1, 300, "", "").ServiceCodings.Select(s => s.ServiceCode).ToList();
                var UnRegisteredService = TopYarTmpViewModel.TopYarTmps.Where(s => !services.Contains(s.ServiceCode)).Select(s => new { s.ServiceCode, s.ServiceName, s.Amount }).Distinct().ToList();
                if (UnRegisteredService.Count > 0)
                {
                    UnregisteredServices = new();
                    foreach (var service in UnRegisteredService)
                    {
                        UnregisteredServices.Add(new CreateAndEditServiceCodeingViewModel()
                        {
                            ServiceName = service.ServiceName,
                            ServiceCode = service.ServiceCode,
                            Amount = long.Parse(service.Amount)
                        });
                    }
                    TopYarTmpViewModel.ServiceMessage = "تعداد " + UnRegisteredService.Count + " خدمت در اطلاعات ثبت شده وجود دارد. لطفا نسبت به ثبت خدمت اقدام نمایید.";
                }


                var accounts = _accountService.GetAccounts(1, 50, "").Accounts.Select(a => a.Iban.Replace("\r\n", "")).ToList();
                var unUsedAccount = TopYarTmpViewModel.TopYarTmps.Where(t => !accounts.Contains(t.Iban)).Select(t => t.Iban).Distinct().ToList();
                if (unUsedAccount.Count > 0)
                    TopYarTmpViewModel.IbanMessage = "لطفا از طریق دکمه حذف اطلاعات اضافی نسبت به حذف حساب های نامرتبط با موسسه اقدام نمایید";
            }


            if (pageId > 1 && pageId != TopYarTmpViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + take;
            }
            else if (pageId == TopYarTmpViewModel.PageCount)
            {
                ViewData["Take"] = ((pageId - 1) * take) + (TopYarTmpViewModel.TopYarTmpCounts % take);
            }
            else
            {
                ViewData["Take"] = take;
            }
        }
    }
}