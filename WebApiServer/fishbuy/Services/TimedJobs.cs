using fishbuy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Services
{
    public class TimedJobs : Job
    {
        private readonly ILogger<TimedJobs> _logger;
        private readonly FishbuyContext _context;
        private readonly QRCodeContext _QRcontext;
        private readonly string _imageFolderPath;

        public TimedJobs(ILogger<TimedJobs> logger, FishbuyContext context,
            QRCodeContext QRcontext, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _QRcontext = QRcontext;
            _imageFolderPath = config.GetSection("AppSettings:ImageFolder").Value;
        }

        /// <summary>
        /// 定时清理过期的 refresh_token
        /// </summary>
        /// <remarks>
        /// Begin: 起始时间；
        /// Interval: 执行时间间隔，单位是毫秒，建议使用以下格式，此处为 24 小时；
        /// SkipWhileExecuting: 是否等待上一个执行完成，true 为等待；
        /// </remarks>
        [Invoke(Begin = "2019-12-16 20:30", Interval = 1000 * 3600 * 24, SkipWhileExecuting = true)]
        public async void CleanExpiredTokenDaily()
        {
            _logger.LogInformation($"{DateTime.Now}: Clean expired refresh token started.");
            var expiredTokens = await _context.RefreshToken.Where(it => it.Expires < DateTime.UtcNow)
                .ToListAsync();
            _logger.LogInformation($"{DateTime.Now}: Find {expiredTokens.Count} token expired.");
            _context.RemoveRange(expiredTokens);
            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.Now}: Clean expired refresh token finished.");
        }

        /// <summary>
        /// 定时清理过期的 QRToken
        /// </summary>
        [Invoke(Begin = "2019-12-16 20:30", Interval = 1000 * 3600 * 1, SkipWhileExecuting = true)]
        public async void CleanExpiredQRToken()
        {
            _logger.LogInformation($"{DateTime.Now}: Clean expired QR token started.");
            var expiredTokens = await _QRcontext.QRTokens.Where(it => it.Expires < DateTime.UtcNow)
                .ToListAsync();
            _logger.LogInformation($"{DateTime.Now}: Find {expiredTokens.Count} token expired.");
            _QRcontext.RemoveRange(expiredTokens);
            _QRcontext.SaveChanges();
            _logger.LogInformation($"{DateTime.Now}: Clean expired QR token finished.");
        }

        /// <summary>
        /// 定时清理未使用的图片
        /// </summary>
        [Invoke(Begin = "2019-12-16 20:30", Interval = 1000 * 3600 * 24 * 7, SkipWhileExecuting = true)]
        public async void CleanNotUsedImagesWeekly()
        {
            _logger.LogInformation($"{DateTime.Now}: Clean not used images started.");
            var images = await _context.UploadedImage.Where(it => _context.MediaLink.All(it2 => Path.GetFileNameWithoutExtension(it2.ResUri) != it.Hash))
                .ToListAsync();
            _logger.LogInformation($"{DateTime.Now}: Find {images.Count} not used images.");

            foreach (var item in images)
            {
                string filePath = Path.Combine(_imageFolderPath, item.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            _context.RemoveRange(images);
            _context.SaveChanges();
            _logger.LogInformation($"{DateTime.Now}: Clean not used images finished.");
        }
    }
}
