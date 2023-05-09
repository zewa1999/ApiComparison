using ApiComparison.GrpcApi.Services;
using ApiComparison.Infrastructure;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.AddInfrastructureLayer(builder.Configuration);
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AccountService>();
app.MapGrpcService<AddressService>();
app.MapGrpcService<DishService>();
app.MapGrpcService<IngredientService>();
app.MapGrpcService<UserService>();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();