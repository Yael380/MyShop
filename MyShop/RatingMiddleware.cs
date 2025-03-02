using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Services;
using System;
using Azure.Core;
using Entities;
using Microsoft.Extensions.Hosting;

namespace MyShop
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,IRatingServices ratingServices)
        {
            Rating rating = new()
            {
                Host = httpContext.Request.Host.ToString(),
                Method = httpContext.Request.Method.ToString(),
                Path = httpContext.Request.Path.ToString(),
                Referer = httpContext.Request.Headers.Referer.ToString(),
                UserAgent = httpContext.Request.Headers.UserAgent.ToString(),
                RecordDate = DateTime.Now
            };
            await ratingServices.PostRating(rating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
