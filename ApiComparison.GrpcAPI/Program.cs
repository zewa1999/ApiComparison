using ApiComparison.GrpcApi.Interceptors;
using ApiComparison.GrpcApi.Services;
using ApiComparison.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ca sa dai drumul la grpc ui: grpcui -plaintext localhost:5019 in powershell

builder.Services.AddGrpcReflection();

builder.Services.AddInfrastructureLayer(builder.Configuration);
// Add services to the container.
builder.Services.AddGrpc(options =>
{
    {
        options.Interceptors.Add<ServerLoggerInterceptor>();
        options.EnableDetailedErrors = true;
    }
});

var app = builder.Build();

app.MapGrpcReflectionService();

app.MapGrpcService<AccountService>();
app.MapGrpcService<AddressService>();
app.MapGrpcService<DishService>();
app.MapGrpcService<IngredientService>();
app.MapGrpcService<UserService>();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();