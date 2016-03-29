using ProformaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProformaWebAPI.ViewModel
{
    public class CategoryResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }

    public class CompanyResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
    }

    public class CompanyDetailsResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public CompanyDetailsViewModel CompanyDetails { get; set; }
    }

    public class FilterCriteriaResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public List<FilterCriteriaViewModel> Criteria { get; set; }
    }

    public class EmailVerificationResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class RegisterResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class LoginResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }

    public class ForgotPasswordResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
        public string TemporaryPassword { get; set; }
    }

    public class ResetPasswordResponse
    {
        public string MESSAGE { get; set; }
        public string Flag { get; set; }
    }
}