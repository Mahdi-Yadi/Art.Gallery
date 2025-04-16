using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Web.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    #region Constructor

    private readonly IAccountService _accountService;
    private readonly TokenService _tokenService;
    private readonly JwtTokenGenerator _jwt;
    private readonly SiteDBContext _context;

    public AccountController(IAccountService accountService, TokenService tokenService, JwtTokenGenerator jwt, SiteDBContext context)
    {
        _accountService = accountService;
        _tokenService = tokenService;
        _jwt = jwt;
        _context = context;
    }

    #endregion

    //Access to XMLHttpRequest at
    //'http://localhost:5162/register'
    //from origin 'http://localhost:3000' has been blocked by CORS policy:
    //Response to preflight request doesn't pass access control check: Redirect is not allowed for a preflight request.

    #region Register

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                status = "ValidationError",
                message = "داده‌های وارد شده نامعتبر هستند.",
                errors = ModelState
            });
        }

        var res = _accountService.Register(dto);

        switch (res)
        {
            case AccountResult.Success:
                return Ok(new
                {
                    status = "Success",
                    message = "ثبت‌نام با موفقیت انجام شد.",
                    code = res
                });
            case AccountResult.Error:
                return Ok(new
                {
                    status = "Error",
                    message = "خطایی رخ داده",
                    code = res
                });
            case AccountResult.Null:
                return Ok(new
                {
                    status = "InvalidData",
                    message = "اطلاعات وارد شده صحیح نیست!",
                    code = res
                });
            case AccountResult.Exist:
                return Ok(new
                {
                    status = "UserExists",
                    message = "کاربر وارد شده از قبل ثبت نام کرده است.",
                    code = res
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown",
                    message = "خطای ناشناخته!",
                    code = res
                });
        }
    }

    #endregion

    #region Login

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user != null && user.Password == loginDto.Password)
        {
            var jwt = _tokenService.GenerateJwtToken(user.Id);
            var refresh = await _tokenService.GenerateRefreshTokenAsync(user.Id);

            return Ok(new
            {
                token = jwt,
                refreshToken = refresh.Token
            });
        }

        return Unauthorized();
    }

    #endregion

    #region Refresh

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken && !x.IsRevoked && !x.IsUsed);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
            return Unauthorized(new { message = "توکن نامعتبر یا منقضی شده" });

        refreshToken.IsUsed = true;
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();

        var jwt = _tokenService.GenerateJwtToken(refreshToken.UserId);
        var newRefresh = await _tokenService.GenerateRefreshTokenAsync(refreshToken.UserId);

        return Ok(new
        {
            token = jwt,
            refreshToken = newRefresh.Token
        });
    }

    #endregion

    #region 

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                status = "ValidationError",
                message = "داده‌های وارد شده نامعتبر هستند.",
                errors = ModelState
            });
        }

        var res = _accountService.Forgot(dto);

        switch (res)
        {
            case AccountResult.Success:
                return Ok(new
                {
                    status = "Success",
                    message = "ایمیلی جهت بازیابی ارسال گردید",
                    code = res
                });
            case AccountResult.Error:
                return Ok(new
                {
                    status = "Error",
                    message = "خطایی رخ داده",
                    code = res
                });
            case AccountResult.Null:
                return Ok(new
                {
                    status = "InvalidData",
                    message = "اطلاعات وارد شده صحیح نیست!",
                    code = res
                });
            case AccountResult.Exist:
                return Ok(new
                {
                    status = "UserExists",
                    message = "کاربر وارد شده از قبل ثبت نام کرده است.",
                    code = res
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown",
                    message = "خطای ناشناخته!",
                    code = res
                });
        }
    }

    #endregion

    //public IActionResult RsetPassword()
    //{
    //    return View();
    //}

    #region Logout

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        // غیرمعتبر کردن Refresh Token
        await _tokenService.RemoveRefreshTokenAsync(request.RefreshToken);

        // اینجا می‌تونی کاری برای پاک کردن JWT انجام بدی، مثلاً در سمت کلاینت، توکن رو حذف کنی
        return Ok(new { message = "با موفقیت خارج شدید" });
    }

    #endregion

    // کد ریکت سمت کلاینت

    //const logout = async () => {
    //    const refreshToken = localStorage.getItem("refreshToken");

    //    // درخواست به سرور برای خارج کردن کاربر
    //    await fetch('https://your-api-url/logout', {
    //        method: 'POST',
    //        headers:
    //        {
    //            'Content-Type': 'application/json',
    //        },
    //        body: JSON.stringify({ refreshToken }),
    //    });

    //    // حذف توکن‌ها از localStorage
    //    localStorage.removeItem('accessToken');
    //    localStorage.removeItem('refreshToken');

    //    // هدایت به صفحه لاگین
    //    window.location.href = '/login';
    //};


}