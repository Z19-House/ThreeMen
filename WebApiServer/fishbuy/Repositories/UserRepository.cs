using fishbuy.Data;
using fishbuy.Models;
using fishbuy.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FishbuyContext _context;
        private readonly string _imageServer;

        public UserRepository(FishbuyContext context, IConfiguration config)
        {
            _context = context;
            _imageServer = config.GetSection("Server:Images").Value;
        }

        public async Task<User> GetUser(string username)
        {
            var item = await _context.User.FirstOrDefaultAsync(u => u.Username == username || u.UserId.ToString() == username);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<User> UpdateUser(string username, UserLarge user)
        {
            var item = await GetUser(username);
            if (item == null)
            {
                return null;
            }
            item.Nickname = user.Nickname;
            item.Phone = user.Phone;
            item.BirthDate = user.BirthDate;
            item.Sex = user.Sex;
            item.Address = user.Address;
            item.ImageUrl = user.ImageUrl.RemoveServerAddress(_imageServer);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<Post>> GetUserPosts(DateTime beforeDateTime, string username, int skip, int take)
        {
            var item = await GetUser(username);
            if (item == null)
            {
                return null;
            }
            return await _context.Post.Where(it => it.UserId == item.UserId && it.UpTime < beforeDateTime)
                .OrderByDescending(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Post>> GetUserCollection(DateTime beforeDateTime, string username, bool withPrivacy, int skip, int take)
        {
            var item = await GetUser(username);
            if (item == null)
            {
                return null;
            }
            return await _context.Collection.Include(it => it.Post)
                .Where(it => it.UserId == item.UserId && it.CollectionTime < beforeDateTime && (withPrivacy || it.Privacy == 0))
                .Select(it => it.Post)
                .Include(it=>it.User)
                .OrderByDescending(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetUserPostsCount(string username, Expression<Func<Post, bool>> predicate)
        {
            var item = await GetUser(username);
            if (item == null)
            {
                return 0;
            }
            return await _context.Post.Where(it => it.UserId == item.UserId).CountAsync(predicate);
        }

        public async Task<int> GetUserCollectionCount(string username, bool withPrivacy, Expression<Func<Collection, bool>> predicate)
        {
            var item = await GetUser(username);
            if (item == null)
            {
                return 0;
            }
            return await _context.Collection.Where(it => it.UserId == item.UserId && (withPrivacy || it.Privacy == 0)).CountAsync(predicate);
        }
    }
}
