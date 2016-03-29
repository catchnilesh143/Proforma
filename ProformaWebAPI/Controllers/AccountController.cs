using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProformaWebAPI.Models;
using ProformaWebAPI.ViewModel;
using BCrypt.Net;
using ProformaWebAPI.Utils;

namespace ProformaWebAPI.Controllers
{
    public class AccountController : ApiController
    {
        ProformaEntities2 _db = new ProformaEntities2();

        [HttpGet]
        [ActionName("VerifyEmailAddress")]
        public HttpResponseMessage VerifyEmailAddress(string EmailAddress)
        {
            var response = new EmailVerificationResponse(); 
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var _emailAddress = _db.Users.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());
                if (null == _emailAddress)
                {
                    response.Flag = "true";
                    response.MESSAGE = "Email verified";
                }
                else
                {
                    response.Flag = "false";
                    response.MESSAGE = "Email exists";
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ActionName("RegisterUser")]
        public HttpResponseMessage RegisterUser(string EmailAddress, string Password)
        {
            var response = new RegisterResponse();
            if (!string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password))
            {
                var hashed = BCrypt.Net.BCrypt.HashPassword(Password);
                User obj = new User();
                obj.Email = EmailAddress;
                obj.Password = hashed;
                obj.CreatedDate = DateTime.Now;
                _db.Users.Add(obj);
                _db.SaveChanges();
                response.Flag = "true";
                response.MESSAGE = "Registered successfully";
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Failed to register";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login(string UserName,string Password)
        {
            var response = new LoginResponse();
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                var userrecord = _db.Users.FirstOrDefault(a => a.Email.ToLower() == UserName.ToLower());
                if (null != userrecord)
                {
                    bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(Password, userrecord.Password);
                    if (IsPasswordValid)
                    {
                        userrecord.IsActive = true;
                        _db.Entry(userrecord).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                        response.Flag = "true";
                        response.MESSAGE = "Login successful";
                    }
                    else
                    {
                        response.Flag = "false";
                        response.MESSAGE = "Invalid Password";
                    }

                }

                else
                {
                    response.Flag = "false";
                    response.MESSAGE = "Invalid Email Address";
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Login fail";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(string EmailAddress)
        {
            var response = new ForgotPasswordResponse();
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                var user = _db.Users.FirstOrDefault(a => a.Email.ToLower() == EmailAddress.ToLower());
                if (null == user)
                {
                    response.Flag = "false";
                    response.MESSAGE = "Email not exists";
                }
                else
                {
                    var tempPass = KeyGenerator.GetUniqueKey(10);
                    user.TempPassword = tempPass;
                    user.TempPassActiveTill = DateTime.Now.AddDays(7); //Active for one week.
                    _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();

                    response.Flag = "true";
                    response.MESSAGE = "Temporary password generated";
                    response.TemporaryPassword = tempPass;
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public HttpResponseMessage ResetPassword(string TemporaryPassword, string NewPassword)
        {
            var response = new ResetPasswordResponse();
            if (!string.IsNullOrEmpty(TemporaryPassword))
            {
                var user = _db.Users.FirstOrDefault(a => a.TempPassword == TemporaryPassword);
                if (null == user)
                {
                    response.Flag = "false";
                    response.MESSAGE = "Invalid temporary password";
                }
                else
                {
                    if (null != user.TempPassActiveTill && DateTime.Now < user.TempPassActiveTill)
                    {
                        var hashed = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                        user.Password = hashed;
                        user.TempPassword = string.Empty;
                        user.TempPassActiveTill = null;
                        _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        response.Flag = "true";
                        response.MESSAGE = "Password reset successful";                        
                    }
                    else
                    {
                        response.Flag = "false";
                        response.MESSAGE = "Temporary password expired";
                    }
                }
            }
            else
            {
                response.Flag = "false";
                response.MESSAGE = "Insufficient data";
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

}
