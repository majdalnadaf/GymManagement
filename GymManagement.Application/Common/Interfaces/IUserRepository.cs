using GymManagement.Domains.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IUserRepository
    {

         Task AddUserAsync(User user);
         Task<bool> ExistsByEmailAsync(string email);

         Task<User?> GetByIdAsync(Guid id);

         Task<User?> GetByEmailAsync(string email);

        Task UpdateUserAsync(User user);

    }
}
