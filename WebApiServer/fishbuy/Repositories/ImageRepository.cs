using fishbuy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly FishbuyContext _context;

        public ImageRepository(FishbuyContext context)
        {
            _context = context;
        }

        public async Task<UploadedImage> DeleteImage(string imageHash)
        {
            var image = await GetImage(imageHash);
            if (image == null)
            {
                return null;
            }
            _context.UploadedImage.Remove(image);
            _context.SaveChanges();
            return image;
        }

        public async Task<UploadedImage> GetImage(string imageHash)
        {
            return await _context.UploadedImage.FirstOrDefaultAsync(it => it.Hash == imageHash);
        }

        public async Task<bool> ImageExists(string imageHash)
        {
            var image = await GetImage(imageHash);
            if (image == null)
            {
                return false;
            }
            return true;
        }

        public async Task<UploadedImage> SaveImage(string imageHash, string fileName, DateTime dateTime)
        {
            var image = await _context.UploadedImage.AddAsync(new UploadedImage
            {
                Hash = imageHash,
                FileName = fileName,
                UploadTime = dateTime
            });
            return image.Entity;
        }
    }
}
