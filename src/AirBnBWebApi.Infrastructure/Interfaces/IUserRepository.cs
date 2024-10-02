using AirBnBWebApi.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirBnBWebApi.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
