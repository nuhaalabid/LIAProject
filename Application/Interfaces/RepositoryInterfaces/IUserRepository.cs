using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
            Task AddAsync(User user);
            Task UpdateAsync(User user);
            Task<List<User>> GetAllAsync();
            Task<User> GetByIdAsync(int id);
            Task<User> GetByEmailAsync(string email);
    }
    }

