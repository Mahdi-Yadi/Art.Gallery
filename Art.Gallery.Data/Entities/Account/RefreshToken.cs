namespace Art.Gallery.Data.Entities.Account;
public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }

    // ارتباط با کاربر
    public long UserId { get; set; }
}
