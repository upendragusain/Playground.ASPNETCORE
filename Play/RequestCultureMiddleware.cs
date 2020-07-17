using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Play
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        // change the culture for the subsequent middlewares to be affected
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var cultureQuery = httpContext.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // Call the next delegate/middleware in the pipeline
            await next(httpContext);

            string x = "this line should be executed (ont the way back) after all the following middlewares have been executed";
            //httpContext./*Response.Body.Write()*/
        }
    }
}
