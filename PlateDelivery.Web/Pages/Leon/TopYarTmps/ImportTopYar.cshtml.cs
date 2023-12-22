using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlateDelivery.Core.Models.TopYarTmps;
using PlateDelivery.Core.Services.TopYarTmps;
using System.Data;
using System.Data.OleDb;

namespace PlateDelivery.Web.Pages.Leon.TopYarTmps
{
    //[UserRoleChecker]
    //[TestPermissionChecker(new int[] { 1, 10 }, 10)]
    public class ImportTopYarModel : PageModel
    {
        private readonly ITopYarTmpService _topYarTmpService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ImportTopYarModel(ITopYarTmpService topYarTmpService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration configuration)
        {
            _topYarTmpService = topYarTmpService;
            _environment = environment;
            _configuration = configuration;
        }

        [BindProperty]
        public ImportTopYarViewModel TopYarModel { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                string validFile = ".xlsx";
                if (Path.GetExtension(postedFile.FileName) != validFile)
                {
                    ModelState.AddModelError("TopYarModel.TopYarFile", "فقط فایل اکسل مجاز به بارگزاری می باشد");
                    return Page();
                }
                //Create a Folder.
                string path = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                //Read the connection string for the Excel file.
                string conString = _configuration.GetConnectionString("ExcelConnection");
                DataTable dt = new();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new(conString))
                {
                    using (OleDbCommand cmdExcel = new())
                    {
                        using (OleDbDataAdapter odaExcel = new())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                for(int i =0;i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][23] = DateTime.Now;
                }

                //Insert the Data read from the Excel file to Database Table.
                conString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.TopYarTmps";

                        //[OPTIONAL]: Map the Excel columns with that of the database table.
                        sqlBulkCopy.ColumnMappings.Add("RetrivalRef", "RetrivalRef");
                        sqlBulkCopy.ColumnMappings.Add("TrackingNo", "TrackingNo");
                        sqlBulkCopy.ColumnMappings.Add("TransactionDate", "TransactionDate");
                        sqlBulkCopy.ColumnMappings.Add("TransactionTime", "TransactionTime");
                        sqlBulkCopy.ColumnMappings.Add("FinancialDate", "FinancialDate");
                        sqlBulkCopy.ColumnMappings.Add("Iban", "Iban");
                        sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                        sqlBulkCopy.ColumnMappings.Add("PrincipalAmount", "PrincipalAmount");
                        sqlBulkCopy.ColumnMappings.Add("CardNo", "CardNo");
                        sqlBulkCopy.ColumnMappings.Add("Terminal", "Terminal");
                        sqlBulkCopy.ColumnMappings.Add("InstallationPlace", "InstallationPlace");
                        sqlBulkCopy.ColumnMappings.Add("ServiceCode", "ServiceCode");
                        sqlBulkCopy.ColumnMappings.Add("ServiceName", "ServiceName");
                        sqlBulkCopy.ColumnMappings.Add("ProvinceName", "ProvinceName");
                        sqlBulkCopy.ColumnMappings.Add("SubProvince", "SubProvince");
                        sqlBulkCopy.ColumnMappings.Add("ProvinceCode", "ProvinceCode");
                        sqlBulkCopy.ColumnMappings.Add("CertainCode", "CertainCode");
                        sqlBulkCopy.ColumnMappings.Add("CodeLevel4", "CodeLevel4");
                        sqlBulkCopy.ColumnMappings.Add("CodeLevel5", "CodeLevel5");
                        sqlBulkCopy.ColumnMappings.Add("CodeLevel6", "CodeLevel6");
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        sqlBulkCopy.ColumnMappings.Add("TaxAmount", "TaxAmount");
                        sqlBulkCopy.ColumnMappings.Add("IncomeAmount", "IncomeAmount");
                        sqlBulkCopy.ColumnMappings.Add("CreationDate", "CreationDate");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return RedirectToPage("Index");
        }
    }
}