using NftCounter.Api.ApiMiddlewares;
using NftCounter.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStoreOptions(builder.Configuration);
builder.Services.AddApiHandlers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGraphQlClient(builder.Configuration);
builder.Services.AddGrapQlConsuments();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiExceptionMiddleware>();

app.RegisterEndpoints();

app.Run();
