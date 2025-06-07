using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Categories;
using Art.Gallery.Data.Entities.Categories;
namespace Art.Gallery.Core.Services.Categories;
public class CategoryService : ICategoryService
{

    #region Constructor

    private readonly SiteDBContext _db;

    public CategoryService(SiteDBContext db)
    {
        _db = db;
    }

    #endregion

    #region Categories

    public CatResult AddCategory(Category cat)
    {
        try
        {
            if (cat.Name == null)
                return CatResult.Null;

            var category = _db.Categories.FirstOrDefault(c => c.Name == cat.Name);

            if (category != null) return CatResult.Exist;

            cat.Name = cat.Name.ToLower();

            _db.Categories.Add(cat);
            _db.SaveChanges();
            return CatResult.Success;
        }
        catch (Exception)
        {
            return CatResult.Error;
        }
    }

    public CatResult DeleteCategory(long Id)
    {
        try
        {
            if (Id == 0)
                return CatResult.Null;

            var category = _db.Categories.FirstOrDefault(c => c.Id == Id);

            if (category == null) return CatResult.Null;

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return CatResult.Success;
        }
        catch (Exception)
        {
            return CatResult.Error;
        }
    }

    public List<Category> GetCategories()
    {
        try
        {
            var categories = _db.Categories.ToList();
       
            return categories;
        }
        catch (Exception)
        {
            return new List<Category>();
        }
    }

    public Category GetCategory(long Id)
    {
        if (Id == 0)
            return null;

        var category = _db.Categories.FirstOrDefault(c => c.Id == Id);

        if (category == null) return null;

        return category;    
    }

    public Category GetForUpdateCategory(long Id)
    {
        if (Id == 0)
            return null;

        var category = _db.Categories.FirstOrDefault(c => c.Id == Id);

        if (category == null) return null;

        return category;
    }

    public CatResult UpdateCategory(Category cat)
    {
        try
        {
            if (cat.Name == null)
                return CatResult.Null;

            var category = _db.Categories.FirstOrDefault(c => c.Id == cat.Id);

            if (category == null) return CatResult.Exist;

            _db.Categories.Update(cat);
            _db.SaveChanges();

            return CatResult.Success;
        }
        catch (Exception)
        {
            return CatResult.Error;
        }
    }

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }

    #endregion

}