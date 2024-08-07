using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.API.Data;
using ShoppingCart.API.Services.Abstractions;
using ShoppingCart.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load the data layer extensions
builder.Services.LoadDataLayerExtension(builder.Configuration);

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

// HTTP Clients
builder.Services.AddHttpClient("CatalogAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogApiUrl"]);
});

builder.Services.AddHttpClient("AuthenticationAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:AuthenticationApiUrl"]);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();