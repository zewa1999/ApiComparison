using ApiComparison.GraphQLApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransientDependencies(builder.Configuration);
builder.Services.AddGraphQLServer();

var app = builder.Build();

app.UseHttpsRedirection();

app.AddGraphQL();

app.Run();
