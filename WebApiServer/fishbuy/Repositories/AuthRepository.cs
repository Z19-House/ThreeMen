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

        public async Task<string> GetRefreshToken(int userId, string token)
        {
            var savedToken = await _context.RefreshToken.FirstOrDefaultAsync(it => it.Token == token && it.User.UserId == userId);
            if (savedToken == null)
            {
                return null;
            }
            if (savedToken.Expires < DateTime.UtcNow)
            {
                _context.RefreshToken.Remove(savedToken);
                _context.SaveChanges();
                return null;
            }
            return savedToken.Token;
        }

        public async Task<int> SaveRefreshToken(int userId, string token)
        {
            await _context.RefreshToken.AddAsync(new RefreshToken
            {
                Token = token,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(30)
            });
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRefreshToken(int userId, string token)
        {
            var item = await _context.RefreshToken.FirstOrDefaultAsync(it => it.Token == token && it.User.UserId == userId);
            _context.RefreshToken.Remove(item);
            return _context.SaveChanges();
        }

    }
}
