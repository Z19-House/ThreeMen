using fishbuy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly FishbuyContext _context;

        public SearchRepository(FishbuyContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPostsByTitle(string title, DateTime beforeDateTime, int skip, int take)
        {
            return await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .Where(it => it.UpTime < beforeDateTime && it.Title.Contains(title))
                .OrderByDescending(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetPostsByTitleCount(string title, DateTime beforeDateTime)
        {
            return await _context.Post.Where(it => it.UpTime < beforeDateTime && it.Title.Contains(title))
                .CountAsync();
        }

        public async Task<List<Post>> GetPostsByTag(string tag, DateTime beforeDateTime, int skip, int take)
        {
            return await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .Where(it => it.UpTime < beforeDateTime && it.Tags.Contains(tag))
                .OrderByDescending(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetPostsByTagCount(string tag, DateTime beforeDateTime)
        {
            return await _context.Post.Where(it => it.UpTime < beforeDateTime && it.Tags.Contains(tag))
                .CountAsync();
        }

        public async Task<List<User>> GetUsersByNickname(string nickname, int skip, int take)
        {
            return await _context.User.Where(it => it.Nickname.Contains(nickname))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetUsersByNicknameCount(string nickname)
        {
            return await _context.User.Where(it => it.Nickname.Contains(nickname))
                .CountAsync();
        }
    }
}
