namespace MessageBus;

public static class MessageBusExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();

            configureConsumers?.Invoke(busConfig);

            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["RabbitMq"]!), host =>
                {
                    host.Username(configuration["RabbitMq:UserName"]!);
                    host.Password(configuration["RabbitMq:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
