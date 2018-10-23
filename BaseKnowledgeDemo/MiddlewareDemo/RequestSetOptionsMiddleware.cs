using BaseKnowledgeDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BaseKnowledgeDemo.Middleware
{
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private IOptions<AppOptions> _options;

        public RequestSetOptionsMiddleware(RequestDelegate next, IOptions<AppOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("RequestSetOptionsMiddleware.Invoke");

            var option = httpContext.Request.Query["option"];

            if (!string.IsNullOrWhiteSpace(option))
            {
                _options.Value.Option = WebUtility.HtmlEncode(option);
            }

            await _next(httpContext);
        }
    }
}
