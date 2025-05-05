using System.Text;
using System.Text.Json.Serialization;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Core.Services.Categories;
using Art.Gallery.Core.Services.Galleries;
using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Emails;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

#region Data Base

var connectionString = "Data Source=.;Initial Catalog=ArtGalleryDB;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=True;";
//var connectionString = "Server=185.164.73.239;Initial Catalog=ArtGalleryDB;User Id=f#1d!+34$#$fdG;Password=2dx@ff466vVKss;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False";
builder.Services.AddDbContext<SiteDBContext>(options =>
{
    options.UseSqlServer(connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "SiteArtDb"));
});

#endregion

// اضافه کردن CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://technoto.ir", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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
// Account
builder.Services.AddTransient<IAccountService, AccountService>();
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