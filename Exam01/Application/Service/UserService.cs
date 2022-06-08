using Exam01.Application.Repository;
using Exam01.Models;

namespace Exam01.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository,ILogger<UserService> logger){
        _repository = repository;
        _logger=logger;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _repository.GetByEmailAsync(email);
    }
}