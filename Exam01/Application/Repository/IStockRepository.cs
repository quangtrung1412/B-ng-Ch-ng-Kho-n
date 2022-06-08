using Exam01.Application.Models;

namespace Exam01.Application.Repository;


public interface IStockRepository
{
    Task<List<Stock>>ListAsync();
    Task<Stock> GetByIdAsync(string Id);
    Task<Stock> UpdateAsync(Stock stock);
    Task<Stock> AddAsync(Stock stock);
}