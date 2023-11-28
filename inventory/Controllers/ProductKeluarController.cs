using Microsoft.AspNetCore.Mvc;
using inventory.Authorization;
using inventory.Entities;
using inventory.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace inventory.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductKeluarController : ControllerBase
{
    private readonly IProductKeluarService _productKeluarService;

    public ProductKeluarController(IProductKeluarService productKeluarService)
    {
        _productKeluarService = productKeluarService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductKeluar()
    {
        var productKeluar = await _productKeluarService.GetAllProductKeluar();
        return Ok(productKeluar);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductKeluarById(int id)
    {
        var productKeluar = await _productKeluarService.GetProductKeluarById(id);
        if (productKeluar == null)
        {
            return NotFound();
        }
        return Ok(productKeluar);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductKeluar([FromBody] ProductKeluar productKeluar)
    {
        var createdProductKeluar = await _productKeluarService.CreateProductKeluar(productKeluar);
        return CreatedAtAction(nameof(GetProductKeluarById), new { id = createdProductKeluar.Id }, createdProductKeluar);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductKeluar(int id, [FromBody] ProductKeluar productKeluar)
    {
        var updatedProductKeluar = await _productKeluarService.UpdateProductKeluar(id, productKeluar);
        if (updatedProductKeluar == null)
        {
            return NotFound();
        }
        return Ok(updatedProductKeluar);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductKeluar(int id)
    {
        var result = await _productKeluarService.DeleteProductKeluar(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
