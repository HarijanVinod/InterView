using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewDemo
{
    using System.Web.Mvc;
    using InterviewDemo.Filters;
    using InterviewDemo.Models.DTO;

    [InsuranceClaimSystemAuthorizationFilter, InsuranceClaimSystemExceptionFilter]
    public class BaseController : Controller
    {
        protected ApplicationDTO _ApplicationDTO
        {
            get
            {
                return (User.Identity as InsuranceClaimSystemIdentity)._WebUserSeesion;
            }
        }



    }
}