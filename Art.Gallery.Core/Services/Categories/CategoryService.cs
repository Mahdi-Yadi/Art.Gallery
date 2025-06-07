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

    public CatResult AddCategory(CategoryDto cat)
    {
        try
        {
            if (cat.Name == null)
                return CatResult.Null;

            var category = _db.Categories.FirstOrDefault(c => c.Name == cat.Name);

            if (category != null) return CatResult.Exist;

            Category c = new Category();

            c.Name = cat.Name.ToLower();
            c.Slug = cat.Slug.ToLower();
             
            _db.Categories.Add(c);
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

            var catSeleted = _db.ProductSelectedCategories.Where(a =>
                a.CategoryId == category.Id).ToList();

            _db.ProductSelectedCategories.RemoveRange(catSeleted);
            _db.SaveChanges();

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return CatResult.Success;
        }
        catch (Exception)
        {
            return CatResult.Error;
        }
    }

    public List<CategoryDto> GetCategories()
    {
        try
        {
            var categories = _db.Categories.ToList();
       
            if(categories.Count == 0) return null;

            List<CategoryDto> dtos = new List<CategoryDto>();

            foreach (var item in categories)
            {
                var a = new CategoryDto()
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Slug = item.Slug
                };
                dtos.Add(a);
            }

            return dtos;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public CategoryDto GetCategory(long Id)
    {
        if (Id == 0)
            return null;

        var category = _db.Categories.FirstOrDefault(c => c.Id == Id);

        if (category == null) return null;

        CategoryDto dto = new CategoryDto();

        dto.Slug = category.Slug;
        dto.Name = category.Name;
        dto.Id = category.Id.ToString();

        return dto;    
    }

    public CategoryDto GetForUpdateCategory(long Id)
    {
        if (Id == 0)
            return null;

        var category = _db.Categories.FirstOrDefault(c => c.Id == Id);

        if (category == null) return null;

        CategoryDto dto = new CategoryDto();

        dto.Slug = category.Slug;
        dto.Name = category.Name;
        dto.Id = category.Id.ToString();
        return dto;
    }

    public CatResult UpdateCategory(CategoryDto cat)
    {
        try
        {
            if (cat.Name == null)
                return CatResult.Null;

            var category = _db.Categories.FirstOrDefault(c => c.Id == Convert.ToInt64(cat.Id));

            if (category == null) return CatResult.Exist;

            category.Name = cat.Name;
            category.Slug = cat.Slug;

            _db.Categories.Update(category);
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