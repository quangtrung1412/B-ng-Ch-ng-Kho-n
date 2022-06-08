using Exam01.Models;

namespace Exam01.Application.Repository;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
}