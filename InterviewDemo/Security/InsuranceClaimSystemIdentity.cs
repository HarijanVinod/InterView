
namespace InterviewDemo
{
    using InterviewDemo.Models.DTO;
    using System;
    using System.Web.Security;

    /// <summary>
    /// Identity used for creating Secure Identity token key to Authenticate & Authorize user
    /// </summary>
    public class InsuranceClaimSystemIdentity : FormsIdentity
    {
        public ApplicationDTO _WebUserSeesion
        {
            get;
            private set;
        }

        public InsuranceClaimSystemIdentity(FormsAuthenticationTicket ticket, ApplicationDTO webUserDTO)
            : base(ticket)
        {          
            this._WebUserSeesion = webUserDTO;            
        }
    }
}
