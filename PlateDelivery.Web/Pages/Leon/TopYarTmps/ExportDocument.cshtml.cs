using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Documents;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;
using System.Globalization;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps;
public class ExportDocumentModel : PageModel
{
    private readonly IDocumentService _documentService;

    public ExportDocumentModel(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public SummaryExportDocumentViewModel SummaryExportDocumentViewModel { get; set; }

    [BindProperty]
    public DocumentYears DocumentYears { get; set; }

    [BindProperty]
    public DocumentMonth DocumentMonth { get; set; }
    public void OnGet(int pageId = 1, int take = 31
        , DocumentYears Year = DocumentYears.NotSelected, DocumentMonth Month = DocumentMonth.NotSelected)
    {
        
        ViewData["Title"] = "خروجی اکسل اسناد حسابداری";
        ViewData["DocumentYear"] = DocumentYears.GetSelectList();
        ViewData["DocumentMonth"] = DocumentMonth.GetSelectList();

        if (Request.Query.ContainsKey("fdy"))
        {
            string? s = Request.Query["fdy"];
            if (s != "")
                Year = (DocumentYears)Enum.Parse(typeof(DocumentYears), Request.Query["fdy"]);
        }

        if (Request.Query.ContainsKey("fdm"))
        {
            string? s = Request.Query["fdm"];
            if (s != "")
                Month = (DocumentMonth)Enum.Parse(typeof(DocumentMonth), Request.Query["fdm"]);
        }

        if (Year != DocumentYears.NotSelected)
        {
            DocumentYears = Year;
        }

        else
        {
            PersianCalendar pc = new PersianCalendar();
            var year = pc.GetYear(DateTime.Now);
            switch (year)
            {
                case 1404:
                    Year = DocumentYears.Year1404;
                    break;

                case 1405:
                    Year = DocumentYears.Year1405;
                    break;

                case 1406:
                    Year = DocumentYears.Year1406;
                    break;

                case 1407:
                    Year = DocumentYears.Year1407;
                    break;

                case 1408:
                    Year = DocumentYears.Year1408;
                    break;

                case 1409:
                    Year = DocumentYears.Year1409;
                    break;

                case 1410:
                    Year = DocumentYears.Year1410;
                    break;

                default:
                    break;
            }
        }

        if (Month != DocumentMonth.NotSelected)
        {
            DocumentMonth = Month;
        }

        SummaryExportDocumentViewModel = _documentService.GetMainHeadListOfDocumentsForExport(pageId, take, Year, Month);
        ViewData["PageID"] = (pageId - 1) * take + 1;

        if (pageId > 1 && pageId != SummaryExportDocumentViewModel.PageCount)
        {
            ViewData["Take"] = ((pageId - 1) * take) + take;
        }
        else if (pageId == SummaryExportDocumentViewModel.PageCount)
        {
            ViewData["Take"] = ((pageId - 1) * take) + (SummaryExportDocumentViewModel.SummaryCounts % take);
        }
        else
        {
            ViewData["Take"] = take;
        }
    }
}
