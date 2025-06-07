using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Account;
namespace Art.Gallery.Data.Dtos.Account;
public class FilterUsersDto : BasePaging
{
    
    public List<UserDto> Users { get; set; }

    public string UserName { get; set; }

    public int Count { get; set; }

    public FilterUsersDto SetUsers(List<UserDto> users)
    {
        this.Users = users;
        return this;
    }

    public FilterUsersDto SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllEntitiesCount = paging.AllEntitiesCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
        this.TakeEntity = paging.TakeEntity;
        this.SkipEntity = paging.SkipEntity;
        this.PageCount = paging.PageCount;
        return this;
    }

}
public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsAdmin { get; set; }
}