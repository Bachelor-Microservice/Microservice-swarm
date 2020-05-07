using System;
using System.IO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.IdentityModel.Logging;

namespace Gateway
{
   public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   config
                       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                       .AddJsonFile("ocelot.json")
                       .AddEnvironmentVariables();
               })
               .ConfigureServices(s =>
               {
                   var authenticationProviderKey = "TestKey";
                 
                    s.AddAuthentication()
                    .AddIdentityServerAuthentication(authenticationProviderKey, x =>
                        {
                            x.Authority = "http://identityservice:5010";
                            x.RequireHttpsMetadata=false;
                            
                        });
                    /*
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudiences = new[] {"item"}
                    };
                    */
                       
                   s.AddOcelot();
                   s.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "PriceCalendarService" });
            });
                 
                   
               })
                .Configure(a =>
                {
                    
                    /*
                    var fordwardedHeaderOptions = new ForwardedHeadersOptions
                    {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                        } ;
                    fordwardedHeaderOptions.KnownNetworks.Clear();
                    fordwardedHeaderOptions.KnownProxies.Clear();

                    a.UseForwardedHeaders(fordwardedHeaderOptions);
                    */
                    IdentityModelEventSource.ShowPII = true; 
                    a.UseWebSockets();
                    a.UseOcelot().Wait();
                    a.UseSwagger();
                    a.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PriceCalendarService");
                    });
                })
                .Build();
    }
}
