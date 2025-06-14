﻿using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productService;
    private readonly IAccountService _accountService;
    public ProductsController(IProductsService productService,
        IAccountService accountService)
    {
        _productService = productService;
        _accountService = accountService;
    }

    [HttpGet("Active-Product/{productId}/{artistId}/{userId}")]
    public IActionResult ActiveProduct(string productId, string artistId,string userId)
    {
        if (string.IsNullOrEmpty(artistId))
            return BadRequest();

        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _productService.ActiveProduct(productId, artistId);

        switch (result)
        {
            case ProductResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ProductResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ProductResult.Null:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpGet("Reject-Product/{productId}/{artistId}/{userId}")]
    public IActionResult RejectProduct(string productId, string artistId, string userId)
    {
        if (string.IsNullOrEmpty(artistId))
            return BadRequest();

        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _productService.RejectProduct(productId, artistId);

        switch (result)
        {
            case ProductResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ProductResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ProductResult.Null:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpPost("FilterProducts")]
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        if (!_accountService.IsAdmin(dto.UserId))
            return BadRequest();

        var result = await _productService.FilterProductsAsync(dto);

        return Ok(result);
    }

}