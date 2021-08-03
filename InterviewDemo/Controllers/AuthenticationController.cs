using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using InterviewDemo.Models.DTO;
using InterviewDemo;
using InterviewDemo.Models.BO;

namespace InterviewDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            UsersParamDTO objUsersParamDTO = new UsersParamDTO();
            try
            {
                HttpCookie Isremmember = Request.Cookies["usr"];
                if (Isremmember != null)
                {
                    HttpCookie myCookie = HttpContext.Request.Cookies.Get("usr");
                    if (myCookie != null)
                    {
                        try
                        {
                            var cookieval = myCookie.Values["val"].ToString();
                            string strDecoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(cookieval));
                            var result = JsonConvert.DeserializeObject<UsersParamDTO>(strDecoded);

                            ApplicationDTO applicationDTO = new ApplicationDTO();
                            applicationDTO = ValidateUser(result);
                            if (applicationDTO != null && applicationDTO.ResultCode != 200)
                            {
                                ViewBag.RouteMessage = applicationDTO.ResultMessage;
                            }
                            if (applicationDTO != null && applicationDTO.ResultCode == 200)
                            {
                                SetFormsAuthenticationCookies(objUsersParamDTO, applicationDTO);
                                return RedirectToAction("GetLossTypeData", "LoadLossTypeDetails");
                            }
                        }
                        catch (Exception ex)
                        {
                            ClearCookies();
                        }
                    }
                }
                HttpCookie appCookies = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (appCookies != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(appCookies.Value);
                    ApplicationDTO authDTO = JsonConvert.DeserializeObject<ApplicationDTO>(authTicket.UserData);
                    if (authDTO != null)
                    {
                        FormsAuthentication.SignOut();
                    }
                }
                ClearCookies();
            }
            catch (Exception ex)
            {

                LogHelper.LogException(ex);
            }
            objUsersParamDTO.RememberMe = true;
            return View(objUsersParamDTO);
        }

        [HttpPost]
        public ActionResult Login(UsersParamDTO usersParamDTO)
        {
            try
            {
                ApplicationDTO applicationDTO = new ApplicationDTO();
                applicationDTO = ValidateUser(usersParamDTO);
                if (applicationDTO != null && applicationDTO.ResultCode != 200)
                {
                    ViewBag.RouteMessage = applicationDTO.ResultMessage;
                }
                if (applicationDTO != null && applicationDTO.ResultCode == 200)
                {
                    SetFormsAuthenticationCookies(usersParamDTO, applicationDTO);
                    return RedirectToAction("GetLossTypeData", "LoadLossTypeDetails");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                ViewBag.RouteMessage = ex.Message;
            }
            return View(usersParamDTO);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            ClearCookies();
            return RedirectToAction("Login", "Authentication");
        }

        [NonAction]
        private void SetFormsAuthenticationCookies(UsersParamDTO objUsersParamDTO, ApplicationDTO applicationDTO)
        {
            var now = DateTime.Now.ToLocalTime();
            var authExp = FormsAuthentication.Timeout.TotalMinutes;

            HttpContext.Response.Headers.Add("Authentication", Guid.NewGuid().ToString());
            HttpContext.Response.Headers.Add("Authorization", Guid.NewGuid().ToString());
            HttpContext.Response.Headers.Add("TokenExpiry", authExp.ToString());
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Authentication,Authorization,TokenExpiry");


            ApplicationDTO appdto = new ApplicationDTO
            {
                AccessToken = string.Empty,
                AuthToken = string.Empty,
                UserName = applicationDTO.UserName,
                RememberMe = applicationDTO.RememberMe,
                IsAuthenticated = applicationDTO.IsAuthenticated
            };


            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                applicationDTO.UserName,
                  now,
                now.Add(FormsAuthentication.Timeout),
                objUsersParamDTO.RememberMe,
               Newtonsoft.Json.JsonConvert.SerializeObject(appdto),
                FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Response.Cookies.Add(cookie);

            //Added For RememberMe            

            if (objUsersParamDTO.RememberMe)
            {
                HttpCookie myCookie = new HttpCookie("usr");
                myCookie.Expires = DateTime.Today.AddDays(15);
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
                SetUserInfoCookies(objUsersParamDTO);
            }

            //Added For RememberMe

        }
        public void ClearCookies()
        {

            if (Request.Cookies["usr"] != null)
            {
                var c = new HttpCookie("usr");
                c.Expires = DateTime.Now.AddDays(-2);
                Response.Cookies.Set(c);
                Request.Cookies.Remove("usr");
            }
            TempData.Clear();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="loginParamDTO"></param>
        /// <returns></returns>
        #region Validate User
        [NonAction]
        public ApplicationDTO ValidateUser(UsersParamDTO usersParamDTO)
        {

            ApplicationDTO applicationDTO = new ApplicationDTO();
            try
            {
                UsersDTO result = new AuthenticationBO().ValidateUser(usersParamDTO);

                if (result != null)
                {
                    applicationDTO.UserName = result.UserName;
                    applicationDTO.ResultMessage = "Sucess";
                    applicationDTO.ResultCode = 200;
                    applicationDTO.IsAuthenticated = true;
                }
                else
                {
                    applicationDTO.ResultMessage = "User Details Not Found";
                    applicationDTO.ResultCode = 404;
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                applicationDTO.ResultMessage = ex.Message;
                applicationDTO.ResultCode = 500;
            }

            return applicationDTO;
        }
        #endregion

        [NonAction]
        public void SetUserInfoCookies(UsersParamDTO objUsersParamDTO)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies.Get("usr");
            string val = JsonConvert.SerializeObject(objUsersParamDTO);
            string strEncoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(val));
            myCookie.Values["val"] = strEncoded;
            HttpContext.Response.SetCookie(myCookie);
        }
    }
}