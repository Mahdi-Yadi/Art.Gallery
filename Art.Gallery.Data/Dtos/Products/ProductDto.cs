namespace Art.Gallery.Data.Dtos.Products;
public class ProductDto
{
    public string Id { get; set; }
    public string ArtistId { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public bool IsSpecial { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }

}