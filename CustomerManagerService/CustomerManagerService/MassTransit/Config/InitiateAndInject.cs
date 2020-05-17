using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagerService.MassTransit.Consumers;

namespace CustomerManagerService.MassTransit.Config
{
    public static class InitiateAndInject
    {
        public static void ConnectToQueue(IServiceCollection services)
        {
            //MASSTRANSIT
            //services.AddScoped<OrderConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreationOfBookingConsumer>();
                x.AddConsumer<DeletionOfBookingConsumer>();
                x.AddConsumer<UpdateOfBookingConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    //URI + details provided by container - set explicit Uri if running with local instance of rabbitmq
                    cfg.Host("rabbitmq");

                    cfg.ReceiveEndpoint(ep =>
                    {
                        ep.ConfigureConsumer<CreationOfBookingConsumer>(provider);
                        ep.ConfigureConsumer<DeletionOfBookingConsumer>(provider);
                        ep.ConfigureConsumer<UpdateOfBookingConsumer>(provider);
                    });

                }));
            });

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();
        }
    }
}
