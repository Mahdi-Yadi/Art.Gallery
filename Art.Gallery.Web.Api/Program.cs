using System.Text;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region Data Base

var connectionString = "Data Source=.;Initial Catalog=ArtGalleryDB;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=True;";
//var connectionString = "Server=1;Initial Catalog=ArtGalleryDB;User Id=username;Password=1234;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False";
builder.Services.AddDbContext<SiteDBContext>(options =>
{
    options.UseSqlServer(connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "SiteArtDb"));
});


#endregion


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


// Services

builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
