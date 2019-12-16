using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface ISearchRepository
    {
        Task<List<Post>> GetPostsByTitle(string title, DateTime beforeDateTime, int skip, int take);
        Task<List<Post>> GetPostsByTag(string tag, DateTime beforeDateTime, int skip, int take);
        Task<List<User>> GetUsersByNickname(string nickname, int skip, int take);
        Task<int> GetPostsByTitleCount(string title, DateTime beforeDateTime);
        Task<int> GetPostsByTagCount(string tag, DateTime beforeDateTime);
        Task<int> GetUsersByNicknameCount(string nickname);
    }
}
