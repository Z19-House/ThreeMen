using fishbuy.Models;
using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(UserSmall user);
        Task<User> SignIn(UserSmall user);
        Task<bool> UserExists(string username);
        Task<string> GetRefreshToken(int userId, string token);
        Task<int> SaveRefreshToken(int userId, string token);
        Task<int> DeleteRefreshToken(int userId, string token);
    }
}
