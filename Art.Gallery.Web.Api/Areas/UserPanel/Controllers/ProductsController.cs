using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
[Area("AdminPanel")]
[Route("api/Admin-Panel/[controller]")]
[ApiController]
//[Authorize]
public class ProductsController : ControllerBase
{
}