using Exam01.Application.Models;
using Exam01.Application.Repository;

namespace Exam01.Application.Service;


public class StockService : IStockService
{
    private const int MinimumNameLength = 3;
    private readonly IStockRepository _repository;
    private readonly ILogger<StockService> _logger;

    public StockService (IStockRepository repository,ILogger<StockService> logger)
    {
        _repository=repository;
        _logger=logger;
    }
    public async Task<Stock> AddAsync(Stock stock)
    {
        return await _repository.AddAsync(stock);
    }

    public async Task<Stock> GetByIdAsync(string Id)
    {
        return await _repository.GetByIdAsync(Id);
    }

    public async Task<List<Stock>> ListAsync()
    {
        return await _repository.ListAsync();
    }

    public async Task<Stock> UpdateAsync(Stock stock)
    {
        return await _repository.UpdateAsync(stock);
    }
}