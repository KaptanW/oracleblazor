using Microsoft.AspNetCore.Mvc;
using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Api.Controllers;

[ApiController]
[Route("api/Ip")]
public class AllowedIpController : ControllerBase
{
    private readonly IAllowedIpsService _allowedIpsService;
    public AllowedIpController(IAllowedIpsService allowedIpsService)
    {
        _allowedIpsService = allowedIpsService;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAllowedIpDto request)
    {
        await _allowedIpsService.CreateAllowedIp(request);
        return Ok();
    }
}