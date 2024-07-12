namespace POC.Infrastructure.Middlewares;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// From https://medium.com/@tarik.nzl/asp-net-core-and-signalr-authentication-with-the-javascript-client-d46c15584daf
public class WebSocketsMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly string[] Endpoints = { "/screen-hub", "/admin-hub" };

    public WebSocketsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;

        // web sockets cannot pass headers so we must take the access token from query param and
        // add it to the header before authentication middleware runs
        if (Endpoints.Any(e => request.Path.StartsWithSegments(e, StringComparison.OrdinalIgnoreCase)) &&
            request.Query.TryGetValue("access_token", out var accessToken))
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        await _next(httpContext);
    }
}