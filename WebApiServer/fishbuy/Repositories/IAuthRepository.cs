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
        Task<string> GetRefreshToken(string username, string token);
        Task<bool> SaveRefreshToken(string username, string token);
        Task<bool> DeleteRefreshToken(string username, string token);
        Task<bool> UpdateRefreshToken(string username, string oldToken, string newToken);
    }
}
