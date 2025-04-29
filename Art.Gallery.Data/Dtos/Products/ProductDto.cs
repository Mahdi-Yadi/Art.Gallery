namespace Art.Gallery.Data.Dtos.Products;
public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public bool IsSpecial { get; set; }
}