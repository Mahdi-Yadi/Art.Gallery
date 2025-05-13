using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Web.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Art.Gallery.Data.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Web.Api.Http;
public class TokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SiteDBContext _context;

    public TokenService(IOptions<JwtSettings> jwtSettings, SiteDBContext context)
    {
        _jwtSettings = jwtSettings.Value;
        _context = context;
    }

    public string GenerateJwtToken(long userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // تغییر از email به userId
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(long userId)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = userId, // تغییر به UserId
            Expires = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.Now,
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken;
    }

    public async Task RemoveRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token != null)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
        }
    }

}