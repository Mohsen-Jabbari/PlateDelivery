using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Documents;
using PlateDelivery.Core.Services.ServiceCodings;
using PlateDelivery.Core.Services.TopYarTmps;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class GenerateDocumentModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;
        private readonly IServiceCodingService _serviceCodingService;
        private readonly IDocumentService _documentService;

        public GenerateDocumentModel(ITopYarTmpService topYarTmpService,
            IServiceCodingService serviceCodingService, IDocumentService documentService)
        {
            _topYarTmpService = topYarTmpService;
            _serviceCodingService = serviceCodingService;
            _documentService = documentService;
        }

        [BindProperty]
        public TopYarTmpViewModel TopYarTmpViewModel { get; set; }
        [BindProperty]
        public ServiceCodingsViewModel ServiceCodingsViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "ایجاد سند حسابداری";
            ServiceCodingsViewModel = _serviceCodingService.GetServiceCodings();
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmpsForDocument();
        }

        public IActionResult OnPost()
        {
            ServiceCodingsViewModel = _serviceCodingService.GetServiceCodings();
            TopYarTmpViewModel = _topYarTmpService.GetTopYarTmpsForDocument();

            foreach (var item in TopYarTmpViewModel.TopYarTmps)
            {
                //پیدا کردن رکورد کامل سرویس مربوط به تراکنش
                var serviceCode = ServiceCodingsViewModel.ServiceCodings
                    .Where(s => s.ServiceCode == item.ServiceCode).ToList();
                if (serviceCode != null)
                {
                    //بررسی اینکه سرویس مربوط به کدامیک از سه حالت است
                    switch (serviceCode.Count)
                    {
                        case 0:
                            break;


                        case 1:

                            //ثبت سند سه سطری
                            var service = serviceCode.FirstOrDefault();
                            if (service != null)
                            {
                                long order = _documentService.CreateDocument(item, service);
                                if (order > 0)
                                    _topYarTmpService.DeleteTopYarTmp(item.Id);
                            }
                            break;


                        case 2:

                            //chack that IncludeTax is True or False
                            if (serviceCode.Where(s => s.IncludeTax).Any())
                            {
                                //if True Register document with 5 rows
                                long order = _documentService.CreateDocument(item, serviceCode);
                                if (order > 0)
                                    _topYarTmpService.DeleteTopYarTmp(item.Id);
                            }

                            if (serviceCode.Where(s => s.Amount == item.Amount && !s.IncludeTax).Any())
                            {
                                //if False check amount with ServiceCode and register 2 rows document
                                var selectedService = serviceCode
                                            .Where(s => s.Amount == item.Amount).SingleOrDefault();
                                if (selectedService != null)
                                {
                                    if (selectedService.CodeLevel6 != null)
                                    {
                                        long order = _documentService.CreateIncomeDocument(item, selectedService);
                                        if (order > 0)
                                            _topYarTmpService.DeleteTopYarTmp(item.Id);
                                    }

                                    else
                                    {
                                        long order = _documentService.CreateTaxDocument(item, selectedService);
                                        if (order > 0)
                                            _topYarTmpService.DeleteTopYarTmp(item.Id);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            return RedirectToPage("Index");
        }
    }
}