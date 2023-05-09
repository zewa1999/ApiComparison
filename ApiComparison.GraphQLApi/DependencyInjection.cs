using ApiComparison.GraphQLApi.CommandsQueries;
using ApiComparison.GraphQLApi.Subscriptions;
using ApiComparison.GraphQLApi.Types;
using GraphQL.Server.Ui.Voyager;

namespace ApiComparison.GraphQLApi;

public static class DependencyInjection
{
    public static WebApplication AddGraphQL(this WebApplication app)
    {
        var voyagerOptions = new VoyagerOptions
        {
            GraphQLEndPoint = "/graphql"
        };

        app.MapGraphQL();
        app.UseGraphQLVoyager("/graphql-voyager", voyagerOptions);
        app.UseWebSockets();
        return app;
    }

    public static IServiceCollection AddGraphQLServer(this IServiceCollection services)
    {
        HotChocolateAspNetCoreServiceCollectionExtensions.AddGraphQLServer(services)
            .AddQueryType<Query>()
            .AddType<AccountType>()
            .AddType<AddressType>()
            .AddType<DishType>()
            .AddType<IngredientType>()
            .AddType<UserType>()
            .AddMutationType<Mutation>()
            .AddSubscriptionType<Subscription>()
            .AddFiltering()
            .AddSorting()
            .AddInMemorySubscriptions();

        return services;
    }
}