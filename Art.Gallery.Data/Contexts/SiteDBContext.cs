using Art.Gallery.Data.Entities.Account;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Data.Contexts;
public class SiteDBContext : DbContext
{
    #region Constructor

    public SiteDBContext(DbContextOptions<SiteDBContext> options) : base(options)
    {

    }

    #endregion

    public DbSet<User> Users { get; set; }

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