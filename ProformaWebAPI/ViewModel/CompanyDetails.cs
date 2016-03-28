using ProformaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProformaWebAPI.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<FilterCriteriaViewModel> FilterCriterias { get; set; }
    }

    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PartnerType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class CompanyDetailsViewModel
    {
        public int CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public string PartnerType { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string FTPSite { get; set; }
        public string ArtEmail { get; set; }
        public string OrderEmail { get; set; }
        public string FOBPointInCanada { get; set; }
        public string QuoteinCanadianDollars { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string PrimaryContactExtension { get; set; }
        public string PrimaryContactDirectLine { get; set; }
        public string SecondaryContactName { get; set; }
        public string SecondaryContactEmail { get; set; }
        public string SecondaryContactExtension { get; set; }
        public string SecondaryContactDirectLine { get; set; }
        public string TradeOnly { get; set; }
        public string Union { get; set; }
        public string ASI { get; set; }
        public string PPAI { get; set; }
        public string PPPC { get; set; }
        public string SAGE { get; set; }
        public string UPIC { get; set; }
        public string Certifications { get; set; }
        public string MinorityOwned { get; set; }
        public string ProformaPricing { get; set; }
        public List<ProformaOffer> ProformaOffers { get; set; }
        public string ProductsNCapabilities { get; set; }
        public string Rushor24hour { get; set; }
    }

    public class ProformaOffer
    {
        public string Offer { get; set; }
    }
}