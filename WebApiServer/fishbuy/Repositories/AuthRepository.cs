﻿using fishbuy.Models;
using fishbuy.Data;
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

        public async Task<User> SignIn(UserSmall user)
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

        public async Task<User> Register(UserSmall user)
        {
            // 新增用户
            var addedUser = await _context.User.AddAsync(new User
            {
                Username = user.Username.ToLower(),
                Nickname = user.Username,
                PasswordHash = user.Password.GetMd5Hash(),
                UserGroup = 9
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

        public async Task<string> SaveRefreshToken(int userId, string token)
        {
            var item = await _context.RefreshToken.AddAsync(new RefreshToken
            {
                Token = token,
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(30)
            });
            await _context.SaveChangesAsync();
            return item.Entity.Token;
        }

        public async Task<string> DeleteRefreshToken(int userId, string token)
        {
            var item = await _context.RefreshToken.FirstOrDefaultAsync(it => it.Token == token && it.User.UserId == userId);
            _context.RefreshToken.Remove(item);
            _context.SaveChanges();
            return item.Token;
        }

        public async Task<bool> UpdatePassword(int userId, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(it => it.UserId == userId);
            if (user == null)
            {
                return false;
            }
            user.PasswordHash = password.GetMd5Hash();
            _context.SaveChanges();
            return true;
        }
    }
}
