using ItemContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.MassTransit.Config
{
    public static class InitiateAndInject
    {
        public static void ConnectToQueue(IServiceCollection services)
        {
            //MASSTRANSIT
            //services.AddScoped<OrderConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq");
                }));
            });

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IPublishEndpoint>(x => x.GetRequiredService<IBusControl>());
            
            services.AddSingleton<IHostedService, BusService>();
        }
    }
}
