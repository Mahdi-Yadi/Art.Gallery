using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Entities.Site;
namespace Art.Gallery.Core.Services.Sliders;
public interface ISliderService : IAsyncDisposable
{
    SliderResult AddSlider(Slider slider);
    SliderResult UpdateSlider(Slider slider);
    SliderResult DeleteSlider(long id);
    Slider GetSlider(long id);
    List<Slider> GetSliders();
} 
public class SliderService : ISliderService
{
    private readonly SiteDBContext _db;

    public SliderService(SiteDBContext db)
    {
        _db = db;
    }

    public SliderResult AddSlider(Slider slider)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(slider.Title))
                return SliderResult.Null;

            _db.Sliders.Add(slider);
            _db.SaveChanges();
            return SliderResult.Success;
        }
        catch
        {
            return SliderResult.Error;
        }
    }

    public SliderResult UpdateSlider(Slider slider)
    {
        try
        {
            var existing = _db.Sliders.FirstOrDefault(s => s.Id == slider.Id);
            if (existing == null)
                return SliderResult.Null;

            _db.Sliders.Update(slider);
            _db.SaveChanges();
            return SliderResult.Success;
        }
        catch
        {
            return SliderResult.Error;
        }
    }

    public SliderResult DeleteSlider(long id)
    {
        try
        {
            var slider = _db.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null)
                return SliderResult.Null;

            _db.Sliders.Remove(slider);
            _db.SaveChanges();
            return SliderResult.Success;
        }
        catch
        {
            return SliderResult.Error;
        }
    }

    public Slider GetSlider(long id)
    {
        return _db.Sliders.FirstOrDefault(s => s.Id == id);
    }

    public List<Slider> GetSliders()
    {
        return _db.Sliders.ToList();
    }

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }
}
