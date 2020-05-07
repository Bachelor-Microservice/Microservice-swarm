using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PriceCalendarService.Models;
using PriceCalendarService.Services;
using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using PriceCalendarService.Hubs;
using Microsoft.AspNetCore.Http.Connections;

namespace PriceCalendarService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string redisConnectionString = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);
            services.AddControllers();
            System.Console.WriteLine(redisConnectionString);
            

            services.AddSignalR()
            .AddStackExchangeRedis(redisConnectionString+":6379", options => {
        options.Configuration.ChannelPrefix = "MyApp";
                });


            services.AddTransient<PriceCalendarServiceContext>();
            services.AddTransient<IItemDayService, ItemDayService>();
            services.AddScoped<IItemPriceAndCurrencyResponseService, ItemPriceAndCurrencyResponseService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "PriceCalendarService" });
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                }));

        MassTransit.Config.InitiateAndInject.ConnectToQueue(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PriceCalendarService");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHangfireServer();
            app.UseHangfireDashboard();
            
            app.UseCors("CorsPolicy");
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/api/hub" );
            });
        }
    }
}
