using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using NUnit.Framework;

namespace PTrampert.WebApi.Attributes.Test
{
    [TestFixture]
    public class ValidateModelAttributeTests
    {
        private ValidateModelAttribute Attribute { get; set; }

        private HttpActionContext Context { get; set; }

        [SetUp]
        public void Setup()
        {
            Attribute = new ValidateModelAttribute
            {
                TestRequest = new HttpRequestMessage(),
            };
            Context = new HttpActionContext();
        }

        [Test]
        public void ResponseIsNotSetWhenModelStateIsValid()
        {
            Attribute.OnActionExecuting(Context);
            Assert.That(Context.Response, Is.Null);
        }

        [Test]
        public void BadRequestResponseIsReturnedWhenModelStateIsNotValid()
        {
            Context.ModelState.AddModelError("Property", "Something was bad");
            Attribute.OnActionExecuting(Context);
            Assert.That(Context.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}
