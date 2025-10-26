using OracleBlazor.Application.DTOs;
using OracleBlazor.Core.Entities;

namespace OracleBlazor.Application.Interfaces;

public interface IUserService
{
    public Task<string> Login(LoginDto loginDto);
    public Task<User> Create(CreateDto createDto);
}