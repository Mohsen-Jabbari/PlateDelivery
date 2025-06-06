﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.ServiceCodings;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Documents;
using PlateDelivery.Core.Services.ServiceCodings;
using PlateDelivery.Core.Services.TopYarTmps;
using PlateDelivery.DataLayer.Entities.TopYarTmpAgg;

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

            long MaxOrder = _documentService.GetMaxOrder();
            if (MaxOrder == 0)
                MaxOrder = 0;
            else
                MaxOrder--;

            List<List<TopYarTmp>> partitions = TopYarTmpViewModel.TopYarTmps.partition(30000);
            for (int i = 0; i < partitions.Count; i++)
            {
                List<long> Ids = new();
                var partition = partitions[i];

                foreach (var item in partition)
                {
                    MaxOrder++;
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
                                    if (service.IncludeTax)
                                    {
                                        long order = _documentService.CreateDocument(item, service, MaxOrder);
                                        if (order > 0)
                                            Ids.Add(item.Id);
                                        //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                    }

                                    else if (!service.IncludeTax)
                                    {
                                        long order = _documentService.CreateTaxDocument(item, service, MaxOrder);
                                        if (order > 0)
                                            Ids.Add(item.Id);
                                        //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                    }
                                }
                                break;


                            case 2:

                                //چک کردن اینکه آیا دو تا رکورد مقدار بولین مالیات 1 دارند
                                var trueTax = serviceCode.Select(s => s.IncludeTax).ToList();
                                if (!trueTax.Contains(false))
                                {
                                    //if True Register document with 5 rows
                                    long order = _documentService.CreateDocument(item, serviceCode, MaxOrder);
                                    if (order > 0)
                                        Ids.Add(item.Id);
                                    //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                }

                                //یعنی حداقل یکی از وضعیت مالیات ها صفر است
                                if (trueTax.Contains(false))
                                {
                                    //برای حالتی است که دو تا فیلد های بولین مالیات صفر است
                                    if (serviceCode.Where(s => !s.IncludeTax).Select(s => s.IncludeTax).Count() > 1)
                                    {
                                        var selectedService = serviceCode
                                                .Where(s => s.Amount == item.Amount || s.OldAmount == item.Amount).SingleOrDefault();
                                        if (selectedService != null)
                                        {
                                            if (selectedService.CodeLevel6 != null)
                                            {
                                                long order = _documentService.CreateIncomeDocument(item, selectedService, MaxOrder);
                                                if (order > 0)
                                                    Ids.Add(item.Id);
                                                //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                            }

                                            else
                                            {
                                                long order = _documentService.CreateTaxDocument(item, selectedService, MaxOrder);
                                                if (order > 0)
                                                    Ids.Add(item.Id);
                                                //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                            }
                                        }
                                        else
                                        {
                                            foreach (var servic in serviceCode)
                                            {
                                                if (servic.CodeLevel6 != null)
                                                {
                                                    long order = _documentService.CreateIncomeDocument(item, servic, MaxOrder);
                                                    if (order > 0)
                                                        Ids.Add(item.Id);
                                                    //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                                }

                                                else
                                                {
                                                    long order = _documentService.CreateTaxDocument(item, servic, MaxOrder);
                                                    if (order > 0)
                                                        Ids.Add(item.Id);
                                                    //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                                }
                                            }
                                        }
                                    }
                                    //در این حالت یکی از فیلدهای مالیات صفر است و دیگری 1
                                    else
                                    {
                                        long order = _documentService.CreateTaxDocument(item, serviceCode, MaxOrder);
                                        if (order > 0)
                                            Ids.Add(item.Id);
                                        //    _topYarTmpService.DeleteTopYarTmp(item.Id);

                                        //foreach (var servic in serviceCode)
                                        //{
                                        //    if (servic != null && servic.IncludeTax)
                                        //    {
                                        //        long order = _documentService.CreateDocument(item, servic, MaxOrder);
                                        //        if (order > 0)
                                        //            Ids.Add(item.Id);
                                        //        //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                        //    }

                                        //    else if (servic != null && !servic.IncludeTax)
                                        //    {
                                        //        long order = _documentService.CreateTaxDocument(item, servic, MaxOrder);
                                        //        if (order > 0)
                                        //            Ids.Add(item.Id);
                                        //        //    _topYarTmpService.DeleteTopYarTmp(item.Id);
                                        //    }

                                        //    //در این حالت باید سند 4 سطری تولید شود
                                        //}
                                    }
                                }
                                break;

                            //برای حالتی است که خدمت شامل دو رکورد است که فقط باید سند بانک/مالیات بخورد و 
                            //یک رکورد دارد که باید مبلغ مالیات از آن کم شود و سند بانک/درآمد/مالیات بخورد
                            //و در کل یک سند 5 سطری ثبت شود
                            case 3:
                                {
                                    if (serviceCode.Where(s => s.IncludeTax == true).Count() == 1)
                                    {
                                        long ordr = _documentService.CreateTaxTaxIncomeDocument(item, serviceCode, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);
                                    }

                                    if (serviceCode.Where(s => s.IncludeTax == true).Count() == 2)
                                    {
                                        long ordr = _documentService.CreateTaxIncomeIncomeDocument(item, serviceCode, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);
                                    }
                                }

                                break;

                            case 4:
                                {
                                    var partialService = serviceCode.FirstOrDefault(s => s.CertainId == 2 && !s.IncludeTax);
                                    var reminderServices = serviceCode.Where(s => s.Id != partialService.Id).ToList();
                                    if (item.Amount == partialService.Amount ||
                                        item.Amount == partialService.OldAmount)
                                    {
                                        long ordr = _documentService.CreateIncomeDocument(item, partialService, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);

                                    }

                                    long srvcAmount = 0, servcOldAmount = 0;
                                    foreach (var srvv in reminderServices)
                                    {
                                        srvcAmount += long.Parse(srvv.Amount);
                                        servcOldAmount += long.Parse(srvv.OldAmount);
                                    }
                                    if (item.Amount == srvcAmount.ToString() || item.Amount == servcOldAmount.ToString())
                                    {
                                        long ordr = _documentService.CreateTaxDocument(item, reminderServices, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);
                                    }
                                }

                                break;

                            case 5:
                                {
                                    var partialService = serviceCode.FirstOrDefault(s => s.CertainId == 2 && !s.IncludeTax);
                                    var reminderServices = serviceCode.Where(s => s.Id != partialService.Id).ToList();
                                    if (item.Amount == partialService.Amount ||
                                        item.Amount == partialService.OldAmount)
                                    {
                                        long ordr = _documentService.CreateIncomeDocument(item, partialService, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);

                                    }

                                    long srvcAmount = 0, servcOldAmount = 0;
                                    foreach (var srvv in reminderServices)
                                    {
                                        srvcAmount += long.Parse(srvv.Amount);
                                        servcOldAmount += long.Parse(srvv.OldAmount);
                                    }
                                    if (item.Amount == srvcAmount.ToString() || item.Amount == servcOldAmount.ToString())
                                    {
                                        long ordr = _documentService.CreateTaxDocument(item, reminderServices, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);
                                    }
                                }
                                break;

                            case 6:
                                {
                                    var partialService = serviceCode.FirstOrDefault(s => s.CertainId == 2 && !s.IncludeTax);
                                    var reminderServices = serviceCode.Where(s => s.Id != partialService.Id).ToList();
                                    if (item.Amount == partialService.Amount ||
                                        item.Amount == partialService.OldAmount)
                                    {
                                        long ordr = _documentService.CreateIncomeDocument(item, partialService, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);

                                    }

                                    long srvcAmount = 0, servcOldAmount = 0;
                                    foreach (var srvv in reminderServices)
                                    {
                                        srvcAmount += long.Parse(srvv.Amount);
                                        servcOldAmount += long.Parse(srvv.OldAmount);
                                    }
                                    if (item.Amount == srvcAmount.ToString() || item.Amount == servcOldAmount.ToString())
                                    {
                                        long ordr = _documentService.CreateTaxDocument(item, reminderServices, MaxOrder);
                                        if (ordr > 0)
                                            Ids.Add(item.Id);
                                    }
                                }
                                break;
                        }
                    }
                }
                _documentService.SaveChanges();
                _topYarTmpService.DeleteTopYarTmp(Ids);
            }

            return RedirectToPage("Index");
        }
    }
}