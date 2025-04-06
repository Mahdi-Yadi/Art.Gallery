using Art.Gallery.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

#region Data Base

var connectionString = "Data Source=.;Initial Catalog=ArtGallery;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=True;";
//var connectionString = "Server=121243;Initial Catalog=artdb;User Id=use;Password=ee2ews;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False";
builder.Services.AddDbContext<SiteDBContext>(options =>
{
    options.UseSqlServer(connectionString,
        o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "TechnotoBlog"));
});

#endregion

builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// فعال‌سازی CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
