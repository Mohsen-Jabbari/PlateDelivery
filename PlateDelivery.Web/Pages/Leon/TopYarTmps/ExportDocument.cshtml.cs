using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlateDelivery.Core.Convertors;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.Documents;
using PlateDelivery.DataLayer.Entities.DocumentAgg.Enums;

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
