using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(User user) => await _repository.AddAsync(user);

        public async Task UpdateAsync(User user) => await _repository.UpdateAsync(user);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<User?> AuthenticateAsync(string login, string password)
        {
            var user = await _repository.GetByLoginAsync(login);
            return user is not null && user.Password == password ? user : null;
        }
    }
}
