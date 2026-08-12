using Operix.Infrastructure.Extensions;
using Operix.Application.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
