﻿using System.Text;
using System.Text.Json.Serialization;
using Art.Gallery.Common;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Core.Services.Categories;
using Art.Gallery.Core.Services.Galleries;
using Art.Gallery.Core.Services.Jobs;
using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Core.Services.Products;
using Art.Gallery.Core.Services.Requests;
using Art.Gallery.Core.Services.SiteSettings;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Emails;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Parbad.Builder;
using Parbad.Gateway.ZarinPal;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

#region Data Base

builder.Services.AddDbContext<SiteDBContext>(options =>
{
    options.UseSqlServer(
        configuration.GetConnectionString("WebSiteDBConnectionStrings"),
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "SiteArtDb"));
});

#endregion

// اضافه کردن CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://gallery.technoto.org", "https://localhost:5173", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// parbad
builder.Services.AddParbad()
    .ConfigureGateways(geteway =>
    {
        geteway.AddZarinPal()
            .WithAccounts(accounts =>
            {
                accounts.AddInMemory(account =>
                {
                    account.MerchantId = "d0879652-f0af-4da0-b803-000629ef2225";
                    account.IsSandbox = true;
                });
            });
    })
    .ConfigureHttpContext(build => build.UseDefaultAspNetCore())
    .ConfigureStorage(build => build.UseMemoryCache());

builder.Services.AddInfrastructure();
builder.Services.AddSingleton<UrlProtector>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Mail
builder.Services.AddTransient<IMailSender, MailSender>();
// render view
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<TokenService>();
// Product
builder.Services.AddTransient<IProductsService, ProductsService>();
// Orders
builder.Services.AddTransient<IOrderService, OrderService>();
// Galleries
builder.Services.AddTransient<IGalleriesSerivce, GalleriesSerivce>();
// Categories
builder.Services.AddTransient<ICategoryService, CategoryService>();
// Artist
builder.Services.AddTransient<IArtistService, ArtistService>();
// Site Setting
builder.Services.AddTransient<ISiteSettingService, SiteSettingService>();
builder.Services.AddScoped<IRequestService, RequestService>();
// Account
builder.Services.AddScoped<IAccountService, AccountService>();
//builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "clientapp/dist"; });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation v1");
    });
}
app.UseHsts();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

//app.Use(async (context, next) =>
//{
//    await next();
//    if (context.Response.StatusCode == 404 &&
//        !context.Request.Path.Value.StartsWith("/api"))
//    {
//        context.Request.Path = "/index.html";
//        await next();
//    }
//});

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

//var spaPath = "/app";
//if (app.Environment.IsDevelopment())
//{
//    app.MapWhen(y => y.Request.Path.StartsWithSegments(spaPath), client =>
//    {
//        client.UseSpa(spa =>
//        {
//            spa.UseProxyToSpaDevelopmentServer("https://localhost:6363");
//        });
//    });
//}
//else
//{
//    app.Map(new PathString(spaPath), client =>
//    {
//        client.UseSpaStaticFiles();
//        client.UseSpa(spa => {
//            spa.Options.SourcePath = "clientapp";

//            // adds no-store header to index page to prevent deployment issues (prevent linking to old .js files)
//            // .js and other static resources are still cached by the browser
//            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
//            {
//                OnPrepareResponse = ctx =>
//                {
//                    ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();
//                    headers.CacheControl = new CacheControlHeaderValue
//                    {
//                        NoCache = true,
//                        NoStore = true,
//                        MustRevalidate = true
//                    };
//                }
//            };
//        });
//    });
//}

app.Run();