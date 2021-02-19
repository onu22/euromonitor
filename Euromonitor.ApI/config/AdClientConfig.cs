using System;
namespace Euromonitor.ApI.config
{

    public class AdClientConfig
    {
        public string RedirectUrl { get; set; }
        public string Clientid { get; set; }
        public string ClientSecret { get; set; }
        public string Resource { get; set; }
        public string Tenant { get; set; }


        public string GetAuthorizeEndpoint() => $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/authorize?response_type=code&client_id={Clientid}&redirect_uri={RedirectUrl}&scope=openid";

        public string GetAuthorityEndpoint() => $"https://login.microsoftonline.com/{Tenant}/oauth2/v2.0/token";

    }

}
