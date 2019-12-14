using fishbuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IImageRepository
    {
        Task<UploadedImage> GetImage(string imageHash);
        Task<UploadedImage> SaveImage(string imageHash, string fileName, DateTime dateTime);
        Task<UploadedImage> DeleteImage(string imageHash);
        Task<bool> ImageExists(string imageHash);
    }
}
