using fishbuy.Dtos;
using fishbuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(UserForAuthDto user);
        Task<User> SignIn(UserForAuthDto user);
        Task<bool> UserExists(string username);
        Task<string> GetRefreshToken(int userId, string token);
        Task<int> SaveRefreshToken(int userId, string token);
        Task<int> DeleteRefreshToken(int userId, string token);
    }
}
