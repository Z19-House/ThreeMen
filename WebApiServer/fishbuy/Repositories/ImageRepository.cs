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

        public async Task<int> DeleteImage(string imageHash)
        {
            var image = await GetImage(imageHash);
            _context.UploadedImage.Remove(image);
            return _context.SaveChanges();
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

        public async Task<int> SaveImage(string imageHash, string fileName, DateTime dateTime)
        {
            await _context.UploadedImage.AddAsync(new UploadedImage
            {
                Hash = imageHash,
                FileName = fileName,
                UploadTime = dateTime
            });
            return await _context.SaveChangesAsync();
        }
    }
}
