using Exam01.Models;

namespace Exam01.Application.Service;


public interface IUserService
{
    Task<User> GetByEmailAsync(string email);
    
}