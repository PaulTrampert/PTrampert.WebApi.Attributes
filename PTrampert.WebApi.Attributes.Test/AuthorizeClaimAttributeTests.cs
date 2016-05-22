using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using NUnit.Framework;

namespace PTrampert.WebApi.Attributes.Test
{
    [TestFixture]
    public class AuthorizeClaimAttributeTests
    {

        private AuthorizeClaimAttribute Attribute { get; set; }

        private HttpActionContext Context { get; set; }

        private ClaimsIdentity Identity { get; set; }

        private HttpRequestMessage Request { get; set; }

        [SetUp]
        public void Setup()
        {
            Attribute = new AuthorizeClaimAttribute("key", "value");
            Context = new HttpActionContext();
            Request = new HttpRequestMessage();
            Identity = new ClaimsIdentity(new List<Claim> {new Claim("key", "value")}, "Basic");
        }

        [Test]
        public void WhenUserHasClaimResponseIsNotSet()
        {
            Attribute.AuthorizeClaim(Context, Identity, Request);
            Assert.That(Context.Response, Is.Null);
        }

        [Test]
        public void WhenUserDoesntHaveClaimResponseIsForbidden()
        {
            Attribute.AuthorizeClaim(Context, new ClaimsIdentity(new List<Claim>(), "Basic"), Request);
            Assert.That(Context.Response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [Test]
        public void WhenUserIsUnauthenticatedResponseIsUnauthorized()
        {
            Attribute.AuthorizeClaim(Context, new ClaimsIdentity(), Request);
            Assert.That(Context.Response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public void WhenUserIsNullResponseIsUnauthorized()
        {
            Attribute.AuthorizeClaim(Context, null, Request);
            Assert.That(Context.Response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}
