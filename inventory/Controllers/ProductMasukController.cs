using Microsoft.AspNetCore.Mvc;
using inventory.Authorization;
using inventory.Entities;
using inventory.Services;

namespace inventory.Controllers;



[ApiController]
[Route("[controller]")]
public class ProductMasukController : ControllerBase
{
    private readonly IProductMasukService _productMasukService;

    public ProductMasukController(IProductMasukService productMasukService)
    {
        _productMasukService = productMasukService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductMasuk()
    {
        var productMasuk = await _productMasukService.GetAllProductMasuk();
        return Ok(productMasuk);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductMasukById(int id)
    {
        var productMasuk = await _productMasukService.GetProductMasukById(id);
        if (productMasuk == null)
        {
            return NotFound();
        }
        return Ok(productMasuk);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductMasuk([FromBody] ProductMasuk productMasuk)
    {
        var createdProductMasuk = await _productMasukService.CreateProductMasuk(productMasuk);
        return CreatedAtAction(nameof(GetProductMasukById), new { id = createdProductMasuk.Id }, createdProductMasuk);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductMasuk(int id, [FromBody] ProductMasuk productMasuk)
    {
        var updatedProductMasuk = await _productMasukService.UpdateProductMasuk(id, productMasuk);
        if (updatedProductMasuk == null)
        {
            return NotFound();
        }
        return Ok(updatedProductMasuk);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductMasuk(int id)
    {
        var result = await _productMasukService.DeleteProductMasuk(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
