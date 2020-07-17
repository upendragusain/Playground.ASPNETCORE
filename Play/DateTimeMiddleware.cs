using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Play
{
    internal class DateTimeMiddleware
    {
        private readonly RequestDelegate next;

        public DateTimeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync($"{21.99:c2}");
        }
    }
}