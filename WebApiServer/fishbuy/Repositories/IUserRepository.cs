using fishbuy.Data;
using fishbuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username);
        Task<User> UpdateUser(string username, UserLarge user);
        Task<User> DeleteUser(string username);
        Task<List<Post>> GetUserPosts(DateTime beforeDateTime, string username, int skip, int take);
        Task<List<Post>> GetUserCollection(DateTime beforeDateTime, string username, bool withPrivacy, int skip, int take);
        Task<int> GetUserPostsCount(string username, DateTime beforeDateTime);
        Task<int> GetUserCollectionCount(string username, bool withPrivacy, DateTime beforeDateTime);
    }
}
