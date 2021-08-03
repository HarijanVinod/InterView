
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewDemo.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InsuranceClaimSystemExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null)
            {
                if (filterContext.Exception != null)
                {
                    LogHelper.LogException(filterContext.Exception);
                }
            }
        }
    }



}