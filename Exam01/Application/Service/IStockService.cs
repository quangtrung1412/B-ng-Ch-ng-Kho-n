using Exam01.Application.Models;

namespace Exam01.Application.Service;


public interface IStockService
{
    Task<List<Stock>>ListAsync();
    Task<Stock> GetByIdAsync(string Id);
    Task<Stock> UpdateAsync(Stock stock);
    Task<Stock> AddAsync(Stock stock);
}