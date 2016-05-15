using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PTrampert.WebApi.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public HttpRequestMessage TestRequest { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ValidateModel(actionContext, TestRequest ?? actionContext.Request);
        }

        private static void ValidateModel(HttpActionContext actionContext, HttpRequestMessage request)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    actionContext.ModelState);
            }
        }
    }
}
