using Art.Gallery.Common;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Requests;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Web.Api.Http;
using Art.Gallery.Web.Api.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Web.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{

    #region Constructor

    private readonly IAccountService _accountService;
    private readonly TokenService _tokenService;
    private readonly JwtTokenGenerator _jwt;
    private readonly SiteDBContext _context;
    private readonly UrlProtector _urlProtector;
    private readonly IRequestService _requestService;

    public AccountController(IAccountService accountService, TokenService tokenService, JwtTokenGenerator jwt, SiteDBContext context, UrlProtector urlProtector, IRequestService requestService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
        _jwt = jwt;
        _context = context;
        _urlProtector = urlProtector;
        _requestService = requestService;
    }

    #endregion

    //Access to XMLHttpRequest at
    //'http://localhost:5162/register'
    //from origin 'http://localhost:3000' has been blocked by CORS policy:
    //Response to preflight request doesn't pass access control check: Redirect is not allowed for a preflight request.

    // ثبت نام
    #region Register

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
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

        if (await _requestService.CanUserSendRequestAsync(HttpContext.Connection.RemoteIpAddress.ToString(), $"ثبت نام - {dto.Email}"))
        {
            return BadRequest(new
            {
                status = "Blocked"
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

    // فعال سازی
    #region Active Account

    [HttpPost("Active-Account/{activeCode}")]
    public async Task<IActionResult> ActiveAccount(string activeCode)
    {

        if(string.IsNullOrEmpty(activeCode))
            return BadRequest();

        if (await _requestService.CanUserSendRequestAsync(HttpContext.Connection.RemoteIpAddress.ToString(), $"فعال سازی"))
        {
            return BadRequest(new
            {
                status = "Blocked"
            });
        }

        AccountResult res = _accountService.ActiveAccount(activeCode);

        switch (res)
        {
            case AccountResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case AccountResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case AccountResult.Null:
                return Ok(new
                {
                    status = "Null"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    #endregion

    // ورود
    #region Login

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _requestService.CanUserSendRequestAsync(HttpContext.Connection.RemoteIpAddress.ToString(), $"ورود - {loginDto.Email}"))
        {
            return BadRequest(new
            {
                status = "Blocked"
            });
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user != null && user.Password == Hashing.HashPassword($"{loginDto.Password}{user.Salt}"))
        {
            var jwt = _tokenService.GenerateJwtToken(user.Id);
            var refresh = await _tokenService.GenerateRefreshTokenAsync(user.Id);

            return Ok(new
            {
                userId = _urlProtector.Protect(user.Id.ToString()),
                token = jwt,
                refreshToken = refresh.Token,
                username = user.UserName
            });
        }

        return Unauthorized();
    }
    //    const res = await fetch("https://localhost:5001/api/login", {

    //        method: "POST",
    //        headers: { "Content-Type": "application/json" },
    //        body: JSON.stringify({ email, password }),
    //    });

    //const data = await res.json();
    //localStorage.setItem("access_token", data.token);
    //localStorage.setItem("refresh_token", data.refreshToken);

    //fetch("https://localhost:5001/api/user/me", {
    //    headers:
    //    {
    //        Authorization: `Bearer ${ localStorage.getItem("access_token")}`,
    //    },
    //});


    #endregion

    // دریافت توکن جدید
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

    // فراموشی کلمه عبور
    #region Forgot Password

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
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

        if (await _requestService.CanUserSendRequestAsync(HttpContext.Connection.RemoteIpAddress.ToString(), $"فراموشی کلمه عبور- {dto.Email}"))
        {
            return BadRequest(new
            {
                status = "Blocked"
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

    // بازگرداندن کلمه عبور
    #region Reset Password

    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
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

        if (await _requestService.CanUserSendRequestAsync(HttpContext.Connection.RemoteIpAddress.ToString(), $"بازیابی کلمه عبور - {dto.Email}"))
        {
            return BadRequest(new
            {
                status = "Blocked"
            });
        }

        var res = _accountService.Reset(dto);

        switch (res)
        {
            case AccountResult.Success:
                return Ok(new
                {
                    status = "Success",
                    message = "کلمه عبور با موفقیت تغییر پیدا کرد.",
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

    // خروج از حساب
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