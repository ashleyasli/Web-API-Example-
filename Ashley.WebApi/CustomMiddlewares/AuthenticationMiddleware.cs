﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ashley.WebApi.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            //basic ashley:12345
            if(authHeader!=null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                var credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));                    
                }
                catch 
                {
                    context.Response.StatusCode = 500;
                }
                
                var credentials = credentialString.Split(':');
                if (credentials[0] == "ashley" && credentials[1] == "12345")
                {
                    var claims = new[]
                    {
                        new Claim("name", credentials[0]),
                        new Claim(ClaimTypes.Role, "Admin")                       
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }

            await _next(context);
        }
    }
}
