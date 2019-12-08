using fishbuy.Dtos;
using fishbuy.Models;
using fishbuy.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FishbuyContext _context;

        public AuthRepository(FishbuyContext context)
        {
            _context = context;
        }

        public async Task<User> SignIn(UserForAuthDto user)
        {
            var loginUser = await _context.User.FirstOrDefaultAsync(u => (u.Username == user.Username.ToLower() || u.UserId.ToString() == user.Username));
            if (loginUser == null || !VerifyPassword(user.Password, loginUser.PasswordHash))
            {
                return null;
            }
            return loginUser;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            return password.GetMd5Hash() == passwordHash;
        }

        public async Task<User> Register(UserForAuthDto user)
        {
            // 新增用户
            var addedUser = await _context.User.AddAsync(new User
            {
                Username = user.Username.ToLower(),
                Nickname = user.Username,
                PasswordHash = user.Password.GetMd5Hash()
            });
            await _context.SaveChangesAsync();

            return addedUser.Entity;
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => (u.Username == username.ToLower() || u.UserId.ToString() == username));
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Task<string> GetRefreshToken(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveRefreshToken(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRefreshToken(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRefreshToken(string username, string oldToken, string newToken)
        {
            throw new NotImplementedException();
        }
    }
}
