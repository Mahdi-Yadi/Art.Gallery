using Art.Gallery.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Entities.Account;
public class UserRequests : BaseEntity
{

    [Base64String]
    [Length(minimumLength: 1, maximumLength: 200)]
    public string UserIp { get; set; }

    public int Count { get; set; }

    public string Work { get; set; }
}