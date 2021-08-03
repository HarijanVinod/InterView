using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InterviewDemo.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InsuranceClaimSystemAuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext==null || filterContext.HttpContext.User == null 
                || filterContext.HttpContext.User.Identity==null 
                || filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
             
                filterContext.HttpContext.Session["SessionExpired"] = true;
                this.HandleUnauthorizedRequest(filterContext);
            }
            else if (filterContext.HttpContext.User.Identity!=null)
            {
                try
                {
                    var _ApplicationDTO = (filterContext.HttpContext.User.Identity as InsuranceClaimSystemIdentity)._WebUserSeesion;
                    if (_ApplicationDTO == null || _ApplicationDTO.IsAuthenticated==false
                        || _ApplicationDTO.UserName == null || _ApplicationDTO.UserName == string.Empty)
                    {
                        filterContext.HttpContext.Session["SessionExpired"] = true;
                        this.HandleUnauthorizedRequest(filterContext);
                    }
                }
                catch (Exception)
                { 
                }
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = string.Empty;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                url = System.Web.Security.FormsAuthentication.LoginUrl;
                var redirectResult = filterContext.Result as RedirectResult;
                if (filterContext.Result is RedirectResult)
                {
                    // It was a RedirectResult => we need to calculate the url
                    var result = filterContext.Result as RedirectResult;
                    url = UrlHelper.GenerateContentUrl(result.Url, filterContext.HttpContext);
                }
                else if (filterContext.Result is RedirectToRouteResult)
                {
                    // It was a RedirectToRouteResult => we need to calculate
                    // the target url
                    var result = filterContext.Result as RedirectToRouteResult;
                    url = UrlHelper.GenerateUrl(result.RouteName, null, null, result.RouteValues, RouteTable.Routes, filterContext.RequestContext, false);
                }

                filterContext.Result = new JsonResult
                {
                    Data = new { Redirect = url }, 
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
                filterContext.HttpContext.Response.StatusCode = 403; 
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

                return;
            }
            else
            {
                //non-ajax request
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}