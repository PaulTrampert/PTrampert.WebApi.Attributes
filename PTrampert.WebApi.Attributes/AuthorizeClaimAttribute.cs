using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PTrampert.WebApi.Attributes
{
    public class AuthorizeClaimAttribute : AuthorizeAttribute
    {
        private readonly string _key;
        private readonly string _value;

        public AuthorizeClaimAttribute(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public HttpRequestMessage TestRequest { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            AuthorizeClaim(actionContext, actionContext.RequestContext.Principal.Identity as ClaimsIdentity, actionContext.Request);
        }

        public void AuthorizeClaim(HttpActionContext actionContext, ClaimsIdentity identity, HttpRequestMessage request)
        {
            if (identity == null || !identity.IsAuthenticated)
            {
                actionContext.Response = request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            if (!identity.HasClaim(_key, _value))
                actionContext.Response = request.CreateResponse(HttpStatusCode.Forbidden);
        }
    }
}
