using fishbuy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly FishbuyContext _context;

        public CollectionRepository(FishbuyContext context)
        {
            _context = context;
        }

        public async Task<Collection> DeleteCollection(int userId, int postId)
        {
            var item = await _context.Collection.FirstOrDefaultAsync(it => it.UserId == userId && it.PostId == postId);
            if (item == null)
            {
                return null;
            }
            _context.Collection.Remove(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<Collection> GetCollection(int userId, int postId)
        {
            var item = await _context.Collection.FirstOrDefaultAsync(it => it.UserId == userId && it.PostId == postId);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<Collection> SaveCollection(int userId, int postId, int privacy)
        {
            var item = await _context.Collection.AddAsync(new Collection
            {
                UserId = userId,
                PostId = postId,
                Privacy = privacy,
                CollectionTime = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Collection> UpdateCollection(int userId, int postId, int privacy)
        {
            var item = await _context.Collection.FirstOrDefaultAsync(it => it.UserId == userId && it.PostId == postId);
            if (item == null)
            {
                return null;
            }
            item.Privacy = privacy;
            item.CollectionTime = DateTime.UtcNow;
            _context.SaveChanges();
            return item;
        }
    }
}
