using Microsoft.AspNetCore.DataProtection;
namespace Art.Gallery.Common;
public class UrlProtector
{

    private readonly IDataProtector _dataProtector;
    public UrlProtector(IDataProtectionProvider dataProtector)
    {
        _dataProtector = dataProtector.CreateProtector("UrlProt2ector16");
    }

    public string Protect(string input)
    {
        return _dataProtector.Protect(input);
    }

    public string UnProtect(string input)
    {
        return _dataProtector.Unprotect(input);
    }

}