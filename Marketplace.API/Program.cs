using Marketplace.API.Configurations;
using Marketplace.API.Services;
using MarketplaceBL.Services;
using MarketplaceDAL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtService>();
builder.Services.AddSingleton<UnitOfWorkService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearerConfiguration(builder.Configuration);

builder.Services.AddDbContext<MarketplaceDbContext>();
builder.Services
    .AddIdentityConfiguration()
    .AddEntityFrameworkStores<MarketplaceDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<FilterEndpointMiddleware>();

app.UseMiddleware<PathLogger>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
