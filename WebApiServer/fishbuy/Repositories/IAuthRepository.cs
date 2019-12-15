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
        Task<string> SaveRefreshToken(int userId, string token);
        Task<string> DeleteRefreshToken(int userId, string token);
        Task<bool> UpdatePassword(int userId, string password);
    }
}
