using ItemContracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceCalendarService.MassTransit.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Config
{
    public static class InitiateAndInject
    {
        public static void ConnectToQueue(IServiceCollection services)
        {
            //MASSTRANSIT
            //services.AddScoped<OrderConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreationOfItemEntityConsumer>();
                x.AddConsumer<DeletionOfItemEntityConsumers>();
                x.AddConsumer<UpdateOfItemEntityConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    //URI + details provided by container - set explicit Uri if running with local instance of rabbitmq
                    cfg.Host("rabbitmq");

                    cfg.ReceiveEndpoint(ep =>
                    {
                        ep.ConfigureConsumer<CreationOfItemEntityConsumer>(provider);
                        ep.ConfigureConsumer<DeletionOfItemEntityConsumers>(provider);
                        ep.ConfigureConsumer<UpdateOfItemEntityConsumer>(provider);

                        ep.Handler<ItemEntityCreated>(context =>
                        {
                            return Console.Out.WriteLineAsync($"Received: {context.Message.Id}");
                        });
                    });

                    

                }));
            });

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();
        }
    }
}
