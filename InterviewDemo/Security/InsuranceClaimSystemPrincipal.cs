namespace InterviewDemo
{
    using System;
    using System.Security.Principal;

    /// <summary>
    /// Principal creates a unique Identity Secure token Key for Authenticate & Authorize User
    /// </summary>
    public class InsuranceClaimSystemPrincipal : IPrincipal
    {
        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role);
        }

        public InsuranceClaimSystemPrincipal(InsuranceClaimSystemIdentity identity)
        {
            this.Identity = identity;
        }
    }
}
