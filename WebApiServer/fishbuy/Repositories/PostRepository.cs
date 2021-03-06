﻿using fishbuy.Data;
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
            var item = await _context.Post.Include(it => it.Comment)
                .Include(it => it.MediaLink)
                .Include(it => it.Collection)
                .FirstOrDefaultAsync(it => it.PostId == postId);
            if (item == null)
            {
                return null;
            }
            _context.MediaLink.RemoveRange(item.MediaLink);
            _context.Comment.RemoveRange(item.Comment);
            _context.Collection.RemoveRange(item.Collection);
            _context.Post.Remove(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<IEnumerable<Post>> GetPosts(DateTime beforeDateTime, int skip, int take)
        {
            return await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .Where(it => it.UpTime < beforeDateTime)
                .OrderByDescending(it => it.UpTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _context.Post.Include(it => it.User)
                .Include(it => it.MediaLink)
                .FirstOrDefaultAsync(it => it.PostId == postId);
            if (post == null)
            {
                return null;
            }
            var comments = await _context.Comment.Include(it => it.User)
                .Where(it => it.PostId == postId)
                .OrderBy(it => it.UpTime)
                .ToListAsync();
            post.Comment = comments;
            return post;
        }

        public async Task<int> GetPostCount(Expression<Func<Post, bool>> predicate)
        {
            return await _context.Post.CountAsync(predicate);
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
            if (post.Medias != null)
            {
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
            if (post.Medias != null)
            {
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
            return await _context.Comment.Include(it => it.User)
                .FirstOrDefaultAsync(it => it.CommentId == item.Entity.CommentId);
        }
    }
}
