using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageLibrary;

namespace PriceCalendarService.MassTransit
{
   public class TestBus
    {
        public static async Task Test()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq");

                sbc.ReceiveEndpoint("test_queue", ep =>
                {
                    ep.Consumer<MyMessageConsumer>();
                    ep.Handler<Message>(context =>
                    {
                        
                        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                    });
                });
            });

            await bus.StartAsync(); // This is important!

            await bus.Publish(new Message { Text = "FROM_PUBLISHER" });
        }

    }

    public class MyMessageConsumer : IConsumer<Message>
    {
        public async Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine("CONSUMING FROM FUNC");
        }
    }
}