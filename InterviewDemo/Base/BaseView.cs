using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientCare
{
    public class BaseViewPage<T> : WebViewPage<T>
    {
        protected AuthDTO _ApplicationDTO
        {
            get
            {
                return (User.Identity as PatientCareIdentity)._ApplicationDTO;
            }
        }
        public override void Execute()
        {

        }
    }

    public class BaseViewPage : WebViewPage
    {
        protected AuthDTO _ApplicationDTO
        {
            get
            {
                return (User.Identity as PatientCareIdentity)._ApplicationDTO;
            }
        }


        public override void Execute()
        {
        }
    }
}