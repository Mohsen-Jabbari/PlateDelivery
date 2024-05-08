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

            #region بررسی شماره حساب های نامرتبط

            var accounts = _accountService.GetAccounts(1, 50, "").Accounts.Select(a => a.Iban.Replace("\r\n", "")).ToList();
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps();
            var unUsedAccount = TopYarTmpViewModel.TopYarTmps.Where(t => !accounts.Contains(t.Iban)).Select(t => t.Iban).Distinct().ToList();
            if (unUsedAccount.Count > 0)
            {
                TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps(1, 50);
                TopYarTmpViewModel.IbanMessage = "لطفا از طریق دکمه حذف اطلاعات اضافی نسبت به حذف حساب های نامرتبط با موسسه اقدام نمایید";
            }


            #endregion



            else if (TopYarTmpViewModel.ProvinceMessage == null)
            {
                //در این مرحله بررسی میشود آیا رکوردی در تاپ یار وجود دارد که شه و استان ندارد یا خیر
                //این کار تا جایی انجام می شود که تمام رکوردها شهر و استان داشته باشند
                var nullProvince = TopYarTmpViewModel.TopYarTmps.Where(r => r.ProvinceName == null || r.ProvinceName == string.Empty ||
                r.SubProvince == null || r.SubProvince == string.Empty).ToList();
                if (nullProvince.Count != 0)
                {
                    TopYarTmpViewModel = _topYarTmpService.GetTopYarNullProvince(1, 50);
                }

                //بعد از بررسی شهر و استان به این مرحله می رسیم
                else
                {
                    if (TopYarTmpViewModel.IbanMessage == null)
                    {
                        #region بررسی سرویس هایی که در تاپ یار هست ولی در سامانه ثبت نشده

                        var services = _serviceCodingService.GetServiceCodings(1, 300, "", "").ServiceCodings.Select(s => s.ServiceCode).Distinct().ToList();
                        TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps();
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

                        #endregion

                        #region قطعه کد زیر باید بررسی کند که آیا رکوردی وجود دارد که مبلغ آن با مبلغ و خدمت مغایرت دارد یا خیر

                        if (TopYarTmpViewModel.ServiceMessage == null)
                        {
                            //سرویس ها را به صورت کد خدمت و جمع مبلغ لیست می کند
                            var newService = _serviceCodingService.GetServiceCodingsExceptParking(1, 300, "", "").ServiceCodings
                                .GroupBy(s => s.ServiceCode)
                                    .Select(group => new
                                    {
                                        ServiceCode = group.Key,
                                        Amount = group.Sum(s => long.Parse(s.Amount)),
                                        OldAmount = group.Sum(s => long.Parse(s.OldAmount))
                                    }).ToList();
                            List<string> incompatibleRRN = new();
                            List<string> srv = new();
                            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps();

                            foreach (var srvc in newService)
                            {
                                //در این قسمت باید بررسی کنیم خدماتی که بیشتر از یک رکورد دارند آیا جمع مبالغشون
                                //با مبلغ تراکنش برابر هست یا نه
                                //اگر برابر نبود یعنی مبلغ خدمت عوض شده و باید اون خدمات لیست شوند
                                //if (TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                //    .Any(group => group.Key.ServiceCode == srvc.ServiceCode && 
                                //    (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount && group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount)))
                                
                                //بر اساس OldAmount
                                if (TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode})
                                    .Any(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                        group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount && 
                                            group.Any(group => group.ProvinceCode != "Except")))
                                {
                                    if (TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                    .Any(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                        group.Sum(group => long.Parse(group.Amount)) != srvc.Amount && 
                                            group.Any(group => group.ProvinceCode != "Except")))
                                    {
                                        var ratio = _serviceCodingService.GetByServiceCode(srvc.ServiceCode).Select(s => s.Ratio).FirstOrDefault();

                                        if (ratio)
                                        {
                                            var transactions = TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                                .Where(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                                    (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount || 
                                                        group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount) && 
                                                            group.Any(group => group.ProvinceCode != "Except")).ToList();

                                            foreach (var trans in transactions.ToList())
                                            {
                                                var amount = trans.Select(t => t.Amount).FirstOrDefault();
                                                if ((long.Parse(amount) % srvc.Amount == 0) || (long.Parse(amount) % srvc.OldAmount == 0))
                                                {
                                                    transactions.Remove(trans);
                                                }
                                            }
                                            incompatibleRRN.AddRange(transactions.Select(s => s.Key.RetrivalRef).ToList());
                                        }
                                        else
                                        {
                                            incompatibleRRN.AddRange(TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                            .Where(group => group.Key.ServiceCode == srvc.ServiceCode && 
                                                (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount && 
                                                        group.Any(group => group.ProvinceCode != "Except")))
                                                            .Select(s => s.Key.RetrivalRef).ToList());
                                            
                                            incompatibleRRN.AddRange(TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                            .Where(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                                (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount &&
                                                    group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount) &&
                                                        group.Any(group => group.ProvinceCode != "Except"))
                                                            .Select(s => s.Key.RetrivalRef).ToList());
                                            srv.Add(srvc.ServiceCode);
                                        }
                                    }
                                }

                                
                                
                                //بر اساس Amount
                                else if (TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                    .Any(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                        group.Sum(group => long.Parse(group.Amount)) != srvc.Amount && 
                                            group.Any(group => group.ProvinceCode != "Except")))
                                {
                                    if (TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                    .Any(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                        group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount && 
                                            group.Any(group => group.ProvinceCode != "Except")))
                                    {
                                        var ratio = _serviceCodingService.GetByServiceCode(srvc.ServiceCode).Select(s => s.Ratio).FirstOrDefault();

                                        if (ratio)
                                        {
                                            var transactions = TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                                .Where(group => group.Key.ServiceCode == srvc.ServiceCode &&
                                                    (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount || 
                                                        group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount) && 
                                                            group.Any(group => group.ProvinceCode != "Except")).ToList();

                                            foreach (var trans in transactions.ToList())
                                            {
                                                var amount = trans.Select(t => t.Amount).FirstOrDefault();
                                                if ((long.Parse(amount) % srvc.Amount == 0) || (long.Parse(amount) % srvc.OldAmount == 0))
                                                {
                                                    transactions.Remove(trans);
                                                }
                                            }
                                            incompatibleRRN.AddRange(transactions.Select(s => s.Key.RetrivalRef).ToList());
                                        }
                                        else
                                        {
                                            incompatibleRRN.AddRange(TopYarTmpViewModel.TopYarTmps.GroupBy(s => new { s.RetrivalRef, s.ServiceCode })
                                            .Where(group => group.Key.ServiceCode == srvc.ServiceCode && (group.Sum(group => long.Parse(group.Amount)) != srvc.Amount && group.Sum(group => long.Parse(group.Amount)) != srvc.OldAmount)).Select(s => s.Key.RetrivalRef).ToList());
                                            srv.Add(srvc.ServiceCode);
                                        }
                                    }
                                }
                            }

                            if (incompatibleRRN.Count > 0)
                            {
                                TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps(incompatibleRRN);
                                TopYarTmpViewModel.MultiplexMessage = "تعداد " + TopYarTmpViewModel.TopYarTmps.Count + " رکورد از تراکنش ها دارای مبلغ مغایر با خدمت های ثبت شده در سامانه دارند";
                            }

                            else//دیتا برای سند زدن اوکی هست
                            {
                                TopYarTmpViewModel = _topYarTmpService.GetTopYarTmps(pageId, take, filterByRRN, filterByTrackingNo, filterByTransactionDate,
                                    filterByIban, filterByAmount, filterByTerminal, filterByServiceCode,
                                    filterByProvinceName, filterBySubProvince);
                            }
                        }

                        #endregion
                    }
                }


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