using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ReceiveMessagesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add MassTransit to the service collection and configure service collection
                    services.AddMassTransit(cfg =>
                    {
                        // Add bus to the collection
                        cfg.AddBus(ConfigureBus);
                        // Add consumer to the collection
                        cfg.AddConsumer<AudioConsumer>();
                    });

                    // Add IHostedService registration of type BusService
                    services.AddHostedService<BusService>();
                });

        // Configure bus
        private static IBusControl ConfigureBus(IServiceProvider provider)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                    cfg.Host("localhost", "/", h => {
                        h.Username("test");
                        h.Password("test");
                    });

                    cfg.ReceiveEndpoint("audio-converter-endpoint", e =>
                    {
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(x => x.Interval(2, 100));

                        e.Consumer<AudioConsumer>(provider);
                    });
            });
        }
    }
}