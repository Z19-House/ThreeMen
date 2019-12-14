using fishbuy.Data;
using fishbuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts(DateTime beforeDateTime, int skip, int take);
        Task<Post> GetPost(int postId);
        Task<Post> SavePost(PostForUpload post);
        Task<Post> UpdatePost(int postId, PostForUpload post);
        Task<Post> DeletePost(int postId);
        Task<int> GetPostCount();
    }
}
