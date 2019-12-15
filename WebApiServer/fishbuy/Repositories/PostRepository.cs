using fishbuy.Data;
using fishbuy.Models;
using fishbuy.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly FishbuyContext _context;
        private readonly string _imageServer;

        public PostRepository(FishbuyContext context, IConfiguration config)
        {
            _context = context;
            _imageServer = config.GetSection("Server:Images").Value;
        }

        public async Task<Post> DeletePost(int postId)
        {
            var item = await GetPost(postId);
            if (item == null)
            {
                return null;
            }
            _context.Post.Remove(item);
            return item;
        }

        public async Task<IEnumerable<Post>> GetPosts(DateTime beforeDateTime, int skip, int take)
        {
            return await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .Where(it => it.UpTime < beforeDateTime)
                .OrderBy(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .Include(it => it.Comment)
                .Select(it =>
                    new Post
                    {
                        PostId = it.PostId,
                        UserId = it.UserId,
                        UpTime = it.UpTime,
                        EditTime = it.EditTime,
                        Title = it.Title,
                        Content = it.Content,
                        Tags = it.Tags,
                        Status = it.Status,
                        Price = it.Price,
                        Address = it.Address,
                        User = it.User,
                        MediaLink = it.MediaLink,
                        Comment = it.Comment.OrderBy(c => c.UpTime).ToArray()
                    })
                .FirstOrDefaultAsync(it => it.PostId == postId);
        }

        public async Task<int> GetPostCount()
        {
            return await _context.Post.CountAsync();
        }

        public async Task<Post> SavePost(PostForUpload post)
        {
            var item = await _context.Post.AddAsync(new Post
            {
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                Tags = post.Tags,
                Status = post.Status,
                Price = post.Price,
                Address = post.Address,
                UpTime = DateTime.UtcNow,
                EditTime = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
            foreach (var media in post.Medias)
            {
                await _context.MediaLink.AddAsync(new MediaLink
                {
                    MediaId = Guid.NewGuid().ToString(),
                    PostId = item.Entity.PostId,
                    ResType = media.ResType.ToString(),
                    ResUri = media.ResUri.RemoveServerAddress(_imageServer)
                });
            }
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Post> UpdatePost(int postId, PostForUpload post)
        {
            var item = await _context.Post.Include(it => it.MediaLink).FirstOrDefaultAsync(it => it.PostId == postId);
            if (item == null)
            {
                return null;
            }
            item.Title = post.Title;
            item.Content = post.Content;
            item.Tags = post.Tags;
            item.Status = post.Status;
            item.Price = post.Price;
            item.Address = post.Address;
            _context.MediaLink.RemoveRange(item.MediaLink);
            foreach (var media in post.Medias)
            {
                await _context.MediaLink.AddAsync(new MediaLink
                {
                    MediaId = Guid.NewGuid().ToString(),
                    PostId = postId,
                    ResType = media.ResType.ToString(),
                    ResUri = media.ResUri.RemoveServerAddress(_imageServer)
                });
            }
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Comment> DeleteComment(string commentId)
        {
            var item = await _context.Comment.FirstOrDefaultAsync(it => it.CommentId == commentId);
            if (item == null)
            {
                return null;
            }
            _context.Comment.Remove(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<Comment> SaveComment(int postId, int userId, string content)
        {
            var item = await _context.Comment.AddAsync(new Comment
            {
                PostId = postId,
                UserId = userId,
                CommentId = Guid.NewGuid().ToString(),
                UpTime = DateTime.UtcNow,
                Content = content
            });
            await _context.SaveChangesAsync();
            return item.Entity;
        }
    }
}
