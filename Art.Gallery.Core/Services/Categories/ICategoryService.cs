using Art.Gallery.Data.Dtos.Categories;
using Art.Gallery.Data.Entities.Categories;
namespace Art.Gallery.Core.Services.Categories;
public interface ICategoryService : IAsyncDisposable
{

    #region Categories

    CatResult AddCategory(CategoryDto cat);

    CategoryDto GetForUpdateCategory(long Id);

    CatResult UpdateCategory(CategoryDto cat);

    CatResult DeleteCategory(long Id);

    CategoryDto GetCategory(long Id);

    List<CategoryDto> GetCategories();

    #endregion

}