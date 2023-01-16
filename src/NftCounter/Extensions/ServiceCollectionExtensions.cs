using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using NftCounter.Api.ApiHandlers.NftCountHandler;
using NftCounter.Api.Options;
using GraphQL.Client.Serializer.SystemTextJson;
using NftCounter.Api.ApiConsumers.NftCountConsumer;

namespace NftCounter.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddStoreOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<StoreOptions>()
            .BindConfiguration(StoreOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

    }

    public static void AddApiHandlers(this IServiceCollection services)
    {
        services.AddTransient<INftCountHandler, NftCountHandler>();
    }

    public static void AddGraphQlClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGraphQLClient>(s => new GraphQLHttpClient(
            configuration.GetValue<string>("CardanoMainNetUrl"), new SystemTextJsonSerializer()));
    }

    public static void AddGrapQlConsuments(this IServiceCollection services) {
        services.AddTransient<IUtxoConsumer, UtxoConsumer>();
    }
}

