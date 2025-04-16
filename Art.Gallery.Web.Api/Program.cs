using System.Text;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Emails;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#region Data Base

var connectionString = "Data Source=.;Initial Catalog=ArtGalleryDB;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=True;";
//var connectionString = "Server=31.25.90.164\\sqlserver2022;Initial Catalog=technoto_art;User Id=technoto_11art;Password=dsd3@fvsdf453;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False";
builder.Services.AddDbContext<SiteDBContext>(options =>
{
    options.UseSqlServer(connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "SiteArtDb"));
});

#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Documentation",
        Version = "v1"
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
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
builder.Services.AddTransient<IAccountService, AccountService>();
// Mail
builder.Services.AddTransient<IMailSender, MailSender>();
// render view
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// فعال کردن CORS
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation v1");
});

app.MapControllers();

app.Run();