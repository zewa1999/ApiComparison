using ApiComparison.WebApi;
using ApiComparison.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new AggregateExceptionFilterAttribute());
    config.Filters.Add(new EntityNotFoundExceptionFilterAttribute());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();

// Return Problem
// Testat controllere
// Pagination
// Authentication and Authorizationr + API Key authentication.;
// Caching
// Docker
// Replica Set for DBs(Idk if caching also, but we'll see
// Kubernetes
// Frontend