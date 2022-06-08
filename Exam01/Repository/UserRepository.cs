using Exam01.Application.Repository;
using Exam01.Database;
using Exam01.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam01.Repository;




public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext db, ILogger<UserRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        User user = new User();
        try
        {
            if (!string.IsNullOrEmpty(email))
            {
                user = await _db.Users.FirstOrDefaultAsync(e => e.Email.Equals(email));
            }
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}