using Microsoft.AspNetCore.Mvc;
using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Entities;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<LoginResponseDto> Login([FromBody] LoginDto request)
    {
        LoginResponseDto response = new LoginResponseDto()
        {

            Token = await _userService.Login(request)
        };
        return response;
    }

    [HttpPost]
    public async Task<User> Create([FromBody] CreateDto request)
    {
        return await _userService.Create(request);
    }
}