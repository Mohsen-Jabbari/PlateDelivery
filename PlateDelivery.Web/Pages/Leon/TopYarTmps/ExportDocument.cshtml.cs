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
    public void OnGet(int pageId = 1, int take = 50, DocumentYears Year = DocumentYears.NotSelected, DocumentMonth Month = DocumentMonth.NotSelected)
    {
        ViewData["Title"] = "خروجی اکسل اسناد حسابداری";

        ViewData["DocumentYear"] = DocumentYears.GetSelectList();
        ViewData["DocumentMonth"] = DocumentMonth.GetSelectList();

        if (Year != DocumentYears.NotSelected)
        {
            DocumentYears = Year;
        }

        if (Month != DocumentMonth.NotSelected)
        {
            DocumentMonth = Month;
        }

        SummaryExportDocumentViewModel = _documentService.GetMainHeadListOfDocumentsForExport(pageId, take, Year, Month);
    }
}
