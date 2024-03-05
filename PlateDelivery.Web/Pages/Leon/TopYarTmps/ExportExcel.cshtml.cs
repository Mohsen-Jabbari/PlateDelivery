using Azure;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.Documents;
using PlateDelivery.Core.Services.Documents;
using PlateDelivery.DataLayer.Entities.CertainAgg;
using PlateDelivery.DataLayer.Entities.DocumentAgg;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using System.Data;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps;
public class ExportExcelModel : PageModel
{
    private readonly IDocumentService _documentService;

    public ExportExcelModel(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public DocumentViewModel DocumentViewModel { get; set; }
    public List<Document> DocumentsForExport { get; set; }

    [BindProperty]
    public ExcelExportType ExportType { get; set; }

    public void OnGet(string DocDate, int pageId = 1, int take = 200000)
    {
        ViewData["Title"] = "خروجی اکسل";
        ViewData["ExportType"] = ExportType.GetSelectList();
        ViewData["DocDate"] = DocDate;//تاریخ سندهای روز برای خروجی اکسل
        DocumentViewModel = _documentService.GetDocumentsByDocDate(DocDate, pageId, take);
    }

    public IActionResult OnPost(string DocDate)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "خروجی اکسل";
            ViewData["ExportType"] = ExportType.GetSelectList();
            ViewData["DocDate"] = DocDate;//تاریخ سندهای روز برای خروجی اکسل
            DocumentViewModel = _documentService.GetDocumentsByDocDate(DocDate, 1, 200000);
        }

        if (ExportType == ExcelExportType.SiaghType)
        {
            ViewData["DocDate"] = DocDate;
            DocumentsForExport = _documentService.GetDocumentsByDocDate(DocDate);
            //بیاد خروجی بر اساس شماره سند شمارش شود و تقسیم بر 4000 شود و به تعداد خارج قسمت تقسیم
            //باید فایل اکسل ایجاد شود

            var Orders = DocumentsForExport.Select(d => d.Order).Distinct().ToList();
            List<List<long>> partitions = Orders.partition(4000);

            for(int i = 0; i < partitions.Count; i++)
            {
                List<long> partition = partitions[i];

                //هر پارتیشن حاوی 4000 سند می باشد که باید از لیست خروجی دریافت کنیم
                DataTable dt = new("فایل سیاق");
                dt.Columns.AddRange(new DataColumn[10] {
                                        new("کد معین"),
                                        new("سطح 4"),
                                        new("سطح 5"),
                                        new("سطح 6"),
                                        new("شرح"),
                                        new("بد"),
                                        new("بس"),
                                        new(""),
                                        new("کد پیگیری"),
                                        new("تاریخ")
                                         });

                var partitionDocument = DocumentsForExport.Where(d => partition.Contains(d.Order))
                                                    .OrderBy(d => d.Order).ToList();
                #region افزودن رکوردهای بانک

                var bankRecords = partitionDocument.Where(b => b.CertainCode == "10101")
                                .OrderBy(b => b.CodeLevel4).OrderBy(b => b.RetrivalRef).ToList();

                foreach (var item in bankRecords)
                {
                    dt.Rows.Add(item.CertainCode,
                                item.CodeLevel4,
                                item.CodeLevel5,
                                item.CodeLevel6,
                                item.Description,
                                item.Debt,
                                item.Credit,
                                "",
                                item.RetrivalRef,
                                item.TransactionDate.ToStdDate());
                }

                #endregion

                #region افزودن رکوردهای درآمد و مالیات

                var incomeRecords = partitionDocument.Where(b => b.CertainCode != "10101")
                                .GroupBy(d => new
                                {
                                    d.CertainCode,
                                    d.CodeLevel4,
                                    d.CodeLevel5,
                                    d.CodeLevel6,
                                    d.Terminal,
                                    d.Description
                                })
                                .Select(n => new
                                {
                                    n.Key.CertainCode,
                                    n.Key.CodeLevel4,
                                    n.Key.CodeLevel5,
                                    n.Key.CodeLevel6,
                                    n.Key.Terminal,
                                    n.Key.Description,
                                    Debt = n.Sum(s => int.Parse(s.Debt)),
                                    Credit = n.Sum(s => int.Parse(s.Credit))
                                }).ToList();

                foreach (var item in incomeRecords)
                {
                    dt.Rows.Add(item.CertainCode,
                                item.CodeLevel4,
                                item.CodeLevel5,
                                item.CodeLevel6,
                                item.Description,
                                item.Debt,
                                item.Credit,
                                "",
                                "",
                                "");
                }
                #endregion

                dt.DefaultView.Sort = "کد معین";
                dt = dt.DefaultView.ToTable();

                using XLWorkbook wb = new();
                wb.Worksheets.Add(dt);
                using MemoryStream stream = new();
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "فایل اسناد مورخ " + DocDate.ToStdDate() + " بخش " + i.ToString() + ".xlsx");
            }
        }

        else if(ExportType == ExcelExportType.TaxType)
        {
            ViewData["DocDate"] = DocDate;
            DocumentsForExport = _documentService.GetDocumentsByDocDateForTax(DocDate);
            //برای ایجاد فایل اداره مالیات فقط باید رکوردهای مربوط به بانک کد معین 10101 در فایل بیاید
            //رکوردهای بانک مربوط به سندهای 2 سطری مالیات نباید در فایل باشد
            DataTable dt = new("فایل اداره مالیات");
            dt.Columns.AddRange(new DataColumn[3] {
                                        new("مبلغ"),
                                        new("تاریخ"),
                                        new("شرح"),
                                         });

            foreach (var item in DocumentsForExport)
            {
                dt.Rows.Add(item.Debt,
                            item.TransactionDate.ToStdDate(),
                            item.Description);
            }

            dt.DefaultView.Sort = "مبلغ";
            dt = dt.DefaultView.ToTable();

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "فایل اداره مالیات مورخ " + DocDate.ToStdDate() + ".xlsx");
        }

        else if(ExportType == ExcelExportType.GeneralType)
        {
            ViewData["DocDate"] = DocDate;
            DocumentsForExport = _documentService.GetDocumentsByDocDate(DocDate);
            DocumentsForExport = DocumentsForExport.OrderBy(d => d.Order)
                                .ThenBy(d => d.RetrivalRef)
                                .ThenBy(d => d.CertainCode)
                                .ThenBy(d => d.CodeLevel4)
                                .ThenBy(d => d.CodeLevel5)
                                .ThenBy(d => d.CodeLevel6).ToList();
            DataTable dt = new("فایل کلی");
            dt.Columns.AddRange(new DataColumn[10] {
                                        new("کد معین"),
                                        new("سطح 4"),
                                        new("سطح 5"),
                                        new("سطح 6"),
                                        new("شرح"),
                                        new("بد"),
                                        new("بس"),
                                        new(""),
                                        new("کد پیگیری"),
                                        new("تاریخ")
                                         });

            foreach (var item in DocumentsForExport)
            {
                dt.Rows.Add(item.CertainCode,
                            item.CodeLevel4,
                            item.CodeLevel5,
                            item.CodeLevel6,
                            item.Description,
                            item.Debt,
                            item.Credit,
                            "",
                            item.RetrivalRef,
                            item.TransactionDate.ToStdDate());
            }

            dt.DefaultView.Sort = "کد پیگیری";
            dt = dt.DefaultView.ToTable();

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dt);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "فایل کلی " + DocDate.ToStdDate() + ".xlsx");
        }
        return Page();
    }
}
