using Microsoft.AspNetCore.Mvc;
using inventory.Authorization;
using inventory.Entities;
using inventory.Services;

namespace inventory.Controllers;

[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleService.GetAllSales();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(int id)
    {
        var sale = await _saleService.GetSaleById(id);
        if (sale == null)
        {
            return NotFound();
        }
        return Ok(sale);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] Sale sale)
    {
        var createdSale = await _saleService.CreateSale(sale);
        return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.Id }, createdSale);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] Sale sale)
    {
        var updatedSale = await _saleService.UpdateSale(id, sale);
        if (updatedSale == null)
        {
            return NotFound();
        }
        return Ok(updatedSale);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var result = await _saleService.DeleteSale(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
