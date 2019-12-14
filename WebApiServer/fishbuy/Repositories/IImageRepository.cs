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
        Task<int> SaveImage(string imageHash, string fileName, DateTime dateTime);
        Task<int> DeleteImage(string imageHash);
        Task<bool> ImageExists(string imageHash);
    }
}
