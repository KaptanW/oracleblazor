using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OracleBlazor.Application.DTOs;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Core.Filter;

namespace OracleBlazor.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/assets")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;
    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

      
      [HttpPost("list")]
    public async Task<IActionResult> GetAll(AssetsFilter? filter)
        => Ok(await _assetService.ListAsync(filter));
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var asset = await _assetService.GetAsync(id);
        return asset is null ? NotFound() : Ok(asset);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssetCreateDto dto)
    {
        var entity = await _assetService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AssetUpdateDto dto)
    {
        var result = await _assetService.UpdateAsync(dto);
        if (!result) throw new Exception("Asset can not updated");
        return result ? Ok() : BadRequest();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _assetService.DeleteAsync(id);
        return result ? Ok() : BadRequest();
    }
}