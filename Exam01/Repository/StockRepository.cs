using Exam01.Application.Models;
using Exam01.Application.Repository;
using Exam01.Database;
using Microsoft.EntityFrameworkCore;

namespace Exam01.Repository;



public class StockRepository : IStockRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<StockRepository> _logger;

    public StockRepository(AppDbContext db, ILogger<StockRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    public Task<Stock> AddAsync(Stock stock)
    {
        throw new NotImplementedException();
    }

    public async Task<Stock> GetByIdAsync(string Id)
    {
        Stock stock = new Stock();
        try
        {
            if (!string.IsNullOrEmpty(Id))
            {
                stock = await _db.Stocks.FirstOrDefaultAsync(e => e.Id.Equals(Id.ToUpper()));
                _db.Entry<Stock>(stock).State = EntityState.Detached;
            }
            return stock;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<List<Stock>> ListAsync()
    {
        List<Stock> listStock = new List<Stock>();
        try
        {
            listStock = await _db.Stocks.ToListAsync();
            return listStock;
        }
        catch
        {
            return listStock;
        }
    }

    public async Task<Stock> UpdateAsync(Stock stock)
    {
        try
        {
            if (stock != null)
            {
                _db.Stocks.Update(stock);
                await _db.SaveChangesAsync();
            }
            return stock;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
