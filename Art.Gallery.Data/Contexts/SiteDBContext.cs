﻿using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Data.Entities.Articles;
using Art.Gallery.Data.Entities.Artists;
using Art.Gallery.Data.Entities.Categories;
using Art.Gallery.Data.Entities.Orders;
using Art.Gallery.Data.Entities.Products;
using Art.Gallery.Data.Entities.Site;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Data.Contexts;
public class SiteDBContext : DbContext
{

    #region Constructor

    public SiteDBContext(DbContextOptions<SiteDBContext> options) : base(options)
    {

    }

    #endregion

    public DbSet<UserRequests> UserRequests { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductSelectedCategories> ProductSelectedCategories { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<SiteSetting> SiteSettings { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Article> Articles { get; set; }

    public DbSet<ProductGallery> ProductGalleries { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Slider> Sliders { get; set; }

    #region on model creating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ArtGallery");

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        base.OnModelCreating(modelBuilder);
    }

    #endregion

}