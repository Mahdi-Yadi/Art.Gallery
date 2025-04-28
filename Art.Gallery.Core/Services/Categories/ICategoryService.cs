using Art.Gallery.Data.Dtos.Categories;
using Art.Gallery.Data.Entities.Categories;
namespace Art.Gallery.Core.Services.Categories;
public interface ICategoryService : IAsyncDisposable
{

    #region Categories

    CatResult AddCategory(Category cat);

    Category GetForUpdateCategory(long Id);

    CatResult UpdateCategory(Category cat);

    CatResult DeleteCategory(long Id);

    Category GetCategory(long Id);

    List<Category> GetCategories();

    #endregion

}