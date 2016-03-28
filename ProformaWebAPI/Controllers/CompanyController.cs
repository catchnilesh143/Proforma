using ProformaWebAPI.Models;
using ProformaWebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProformaWebAPI.Controllers
{
    public class CompanyController : ApiController
    {
        ProformaEntities2 _db = new ProformaEntities2();

        //api/Company/GetCompaniesByCategory?Id=1&FilterCriteriaId=2
        [HttpGet]
        [ActionName("GetCompaniesByCategory")]
        public HttpResponseMessage GetCompaniesByCategory(int Id, int? FilterCriteriaId)
        {
            List<Company> lstcompany = new List<Company>();
            if (null == FilterCriteriaId || FilterCriteriaId < 1)
            {
                lstcompany = _db.Companies.Where(o => o.CategoryId == Id).ToList();
            }
            else
            {
                lstcompany = (from c in _db.Companies
                              join cm in _db.CompanyMetas
                              on c.CompanyId equals cm.CompanyId
                              where cm.FilterCriteriaId == FilterCriteriaId && c.CategoryId == Id
                              select c).ToList();
            }


            CompanyResponse _CompanyResponse = new CompanyResponse();
            _CompanyResponse.MESSAGE = "Companies By Category";
            _CompanyResponse.Flag = "false";
            if (lstcompany != null)
            {
                List<CompanyViewModel> lstCompanies = new List<CompanyViewModel>();
                _CompanyResponse.Flag = "true";

                for (int i = 0; i < lstcompany.Count(); i++)
                {
                    CompanyViewModel _CompanyViewModel = new CompanyViewModel();
                    _CompanyViewModel.CompanyId = lstcompany[i].CompanyId;
                    _CompanyViewModel.CompanyName = lstcompany[i].CompanyName;
                    _CompanyViewModel.PartnerType = lstcompany[i].PartnerType;
                    _CompanyViewModel.StreetAddress = lstcompany[i].StreetAddress;
                    _CompanyViewModel.City = lstcompany[i].City;
                    _CompanyViewModel.ZipCode = lstcompany[i].ZipCode;
                    _CompanyViewModel.State = lstcompany[i].State;
                    lstCompanies.Add(_CompanyViewModel);
                }
                _CompanyResponse.Companies = lstCompanies;

                return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _CompanyResponse);
        }

        [HttpGet]
        [ActionName("GetCompanyDetailsById")]
        public HttpResponseMessage GetCompanyDetailsById(int Id)
        {
            var company = _db.Companies.FirstOrDefault(o => o.CategoryId == Id);
            CompanyDetailsResponse objCompanyDetails = new CompanyDetailsResponse();
            objCompanyDetails.MESSAGE = "Company Details";
            objCompanyDetails.Flag = "false";
            if (company != null)
            {
                objCompanyDetails.Flag = "true";

                CompanyDetailsViewModel _Company = new CompanyDetailsViewModel();
                _Company.CompanyId = company.CompanyId;
                _Company.PartnerType = company.PartnerType;
                _Company.CompanyName = company.CompanyName;
                _Company.StreetAddress = company.StreetAddress;
                _Company.City = company.City;
                _Company.State = company.State;
                _Company.ZipCode = company.ZipCode;
                _Company.Phone1 = company.Phone1;
                _Company.Phone2 = company.Phone2;
                _Company.Fax = company.Fax;
                _Company.Website = company.Website;
                _Company.FTPSite = company.FTPSite;
                _Company.ArtEmail = company.ArtEmail;
                _Company.OrderEmail = company.OrderEmail;
                _Company.FOBPointInCanada = company.FOBPointInCanada;
                _Company.QuoteinCanadianDollars = company.QuoteinCanadianDollars;
                _Company.PrimaryContactName = company.PrimaryContactName;
                _Company.PrimaryContactEmail = company.PrimaryContactEmail;
                _Company.PrimaryContactExtension = company.PrimaryContactExtension;
                _Company.PrimaryContactDirectLine = company.PrimaryContactDirectLine;
                _Company.SecondaryContactName = company.SecondaryContactName;
                _Company.SecondaryContactEmail = company.SecondaryContactEmail;
                _Company.SecondaryContactExtension = company.SecondaryContactExtension;
                _Company.SecondaryContactDirectLine = company.SecondaryContactDirectLine;
                _Company.TradeOnly = company.TradeOnly;
                _Company.Union = company.Union;
                _Company.ASI = company.ASI;
                _Company.PPAI = company.PPAI;
                _Company.PPPC = company.PPPC;
                _Company.SAGE = company.SAGE;
                _Company.UPIC = company.UPIC;
                _Company.Certifications = company.Certifications;
                _Company.MinorityOwned = company.MinorityOwned;
                _Company.ProformaPricing = company.ProformaPricing;
                List<ProformaOffer> lstProformaPrograms = new List<ProformaOffer>();
                if (!string.IsNullOrEmpty(company.ProformaPrograms))
                {
                    var lst = company.ProformaPrograms.Split(new[] { ',', '.' }).ToList();
                    foreach (var pc in lst)
                    {
                        var strProformaOffer = pc.Trim();
                        if (!string.IsNullOrEmpty(strProformaOffer))
                        {
                            lstProformaPrograms.Add(new ProformaOffer { Offer = strProformaOffer });
                        }
                    }
                }
                _Company.ProformaOffers = lstProformaPrograms;
                _Company.ProductsNCapabilities = company.ProductsNCapabilities;

                _Company.Rushor24hour = company.Rushor24hour;
                _Company.CategoryId = company.CategoryId;
                objCompanyDetails.CompanyDetails = _Company;
            }

            return Request.CreateResponse(HttpStatusCode.OK, objCompanyDetails);
        }
    }
}
