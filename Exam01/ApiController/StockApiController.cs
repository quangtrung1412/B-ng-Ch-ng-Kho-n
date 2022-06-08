using Exam01.Application.Models;
using Exam01.Application.Service;
using Exam01.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Exam01.ApiController;


[ApiController]
[Route("api/Stock")]
public class StockApiController : ControllerBase
{
    private readonly IStockService _service;
    private readonly ILogger<StockApiController> _logger;

    public StockApiController(IStockService service, ILogger<StockApiController> logger)
    {
        _service = service;
        _logger = logger;
    }
    [HttpGet]
    [Route("listStock")]
    public async Task<List<Stock>> ListAsync()
    {
        var listStock = await _service.ListAsync();
        return listStock;
    }
    [HttpPut("updatestock/{id}")]
    public async Task<IActionResult> UpdateStock(string Id, Stock stock)
    {
        if (Id.Equals(stock.Id))
        {
            var data = await _service.UpdateAsync(stock);
            return Ok(stock);
        }
        return BadRequest();


    }
}