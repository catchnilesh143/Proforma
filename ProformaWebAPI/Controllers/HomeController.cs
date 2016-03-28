using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProformaWebAPI.Models;
using System.IO;
using OfficeOpenXml;
using System.Data.Entity.Validation;

namespace ProformaWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ImportExcel()
        {
            ProformaEntities2 _db = new ProformaEntities2();

            //string filePath = Server.MapPath("/Docs/2016MASTERSourceGuideListings.xlsx");
            string filePath = "E:/Gotcha/Docs/2016MASTERSourceGuideListings.xlsx";
            var excelFile = new FileInfo(filePath);

            List<Company> CompanyList = new List<Company>();
            const int startRow = 1;
            try
            {
                using (var package = new ExcelPackage(excelFile))
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            // Get the worksheet
                            foreach (ExcelWorksheet currentWorksheet in workBook.Worksheets)
                            {
                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                                {
                                    var categoryText = currentWorksheet.Name;

                                    string category = Convert.ToString(currentWorksheet.Cells[rowNumber, 1].Value).Trim();
                                    if (!string.IsNullOrEmpty(category))
                                    {
                                        int? categoryId = null;
                                        var cat = _db.Categories.FirstOrDefault(c => c.Category1.ToLower() == categoryText.ToLower());
                                        if (null != cat)
                                        {
                                            categoryId = cat.CategoryId;
                                        }

                                        string MVPLP_PLP = Convert.ToString(currentWorksheet.Cells[rowNumber, 2].Value);
                                        string Company_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 3].Value);
                                        string Street_Address = Convert.ToString(currentWorksheet.Cells[rowNumber, 4].Value);
                                        string City = Convert.ToString(currentWorksheet.Cells[rowNumber, 5].Value);
                                        string State = Convert.ToString(currentWorksheet.Cells[rowNumber, 6].Value);
                                        string ZipCode = Convert.ToString(currentWorksheet.Cells[rowNumber, 7].Value);
                                        string Phone_1 = Convert.ToString(currentWorksheet.Cells[rowNumber, 8].Value);
                                        string Phone_2 = Convert.ToString(currentWorksheet.Cells[rowNumber, 9].Value);
                                        string Fax = Convert.ToString(currentWorksheet.Cells[rowNumber, 10].Value);
                                        string Website = Convert.ToString(currentWorksheet.Cells[rowNumber, 11].Value);
                                        string FTPSite = Convert.ToString(currentWorksheet.Cells[rowNumber, 12].Value);
                                        string ArtMail = Convert.ToString(currentWorksheet.Cells[rowNumber, 13].Value);

                                        string OrderEmail = Convert.ToString(currentWorksheet.Cells[rowNumber, 14].Value);
                                        string FOB_pointinCanada = Convert.ToString(currentWorksheet.Cells[rowNumber, 15].Value);
                                        string CanadianDollers = Convert.ToString(currentWorksheet.Cells[rowNumber, 16].Value);
                                        string PrimaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 17].Value);
                                        string PrimaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 18].Value);
                                        string PrimaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 19].Value);
                                        string PrimaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 20].Value);
                                        string SecondaryContact_Name = Convert.ToString(currentWorksheet.Cells[rowNumber, 21].Value);
                                        string SecondaryContact_Email = Convert.ToString(currentWorksheet.Cells[rowNumber, 22].Value);
                                        string SecondaryContact_Extension = Convert.ToString(currentWorksheet.Cells[rowNumber, 23].Value);
                                        string SecondaryContact_DirectLine = Convert.ToString(currentWorksheet.Cells[rowNumber, 24].Value);
                                        string TradeOnly = Convert.ToString(currentWorksheet.Cells[rowNumber, 25].Value);
                                        string Union = Convert.ToString(currentWorksheet.Cells[rowNumber, 26].Value);

                                        string ASI = Convert.ToString(currentWorksheet.Cells[rowNumber, 27].Value);
                                        string PPAI = Convert.ToString(currentWorksheet.Cells[rowNumber, 28].Value);
                                        string PPPC = Convert.ToString(currentWorksheet.Cells[rowNumber, 29].Value);
                                        string SAGE = Convert.ToString(currentWorksheet.Cells[rowNumber, 30].Value);
                                        string UPIC = Convert.ToString(currentWorksheet.Cells[rowNumber, 31].Value);
                                        string Certification = Convert.ToString(currentWorksheet.Cells[rowNumber, 32].Value);
                                        string Minority_Owned = Convert.ToString(currentWorksheet.Cells[rowNumber, 33].Value);
                                        string Proforma_Pricing = Convert.ToString(currentWorksheet.Cells[rowNumber, 34].Value);
                                        string Proforma_Programs = Convert.ToString(currentWorksheet.Cells[rowNumber, 35].Value);
                                        string Product_capability = Convert.ToString(currentWorksheet.Cells[rowNumber, 36].Value);
                                        string Rushor24hours = Convert.ToString(currentWorksheet.Cells[rowNumber, 37].Value);

                                        Company _objCompany = new Company { CategoryId = categoryId, PartnerType = MVPLP_PLP, CompanyName = Company_Name, StreetAddress = Street_Address, City = City, State = State, ZipCode = ZipCode, Phone1 = Phone_1, Phone2 = Phone_2, Fax = Fax, Website = Website, FTPSite = FTPSite, ArtEmail = ArtMail, OrderEmail = OrderEmail, FOBPointInCanada = FOB_pointinCanada, QuoteinCanadianDollars = CanadianDollers, PrimaryContactName = PrimaryContact_Name, PrimaryContactEmail = PrimaryContact_Email, PrimaryContactExtension = PrimaryContact_Extension, PrimaryContactDirectLine = PrimaryContact_DirectLine, SecondaryContactName = SecondaryContact_Name, SecondaryContactEmail = SecondaryContact_Email, SecondaryContactExtension = SecondaryContact_Extension, SecondaryContactDirectLine = SecondaryContact_DirectLine, TradeOnly = TradeOnly, Union = Union, ASI = ASI, PPAI = PPAI, PPPC = PPPC, SAGE = SAGE, UPIC = UPIC, Certifications = Certification, MinorityOwned = Minority_Owned, ProformaPricing = Proforma_Pricing, ProformaPrograms = Proforma_Programs, ProductsNCapabilities = Product_capability, Rushor24hour = Rushor24hours };

                                        _db.Companies.Add(_objCompany);
                                    }
                                }
                            }
                        }
                    }
                }

                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
               .SelectMany(x => x.ValidationErrors)
               .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return View();
        }
    }
}
