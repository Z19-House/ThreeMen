using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface ICollectionRepository
    {
        Task<Collection> GetCollection(int userId, int postId);
        Task<Collection> SaveCollection(int userId, int postId, int privacy);
        Task<Collection> UpdateCollection(int userId, int postId, int privacy);
        Task<Collection> DeleteCollection(int userId, int postId);
    }
}
