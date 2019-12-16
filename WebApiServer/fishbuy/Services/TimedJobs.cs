using fishbuy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Services
{
    public class TimedJobs : Job
    {
        private readonly ILogger<TimedJobs> _logger;
        private readonly FishbuyContext _context;

        public TimedJobs(ILogger<TimedJobs> logger, FishbuyContext context)
        {
            _context = context;
            _logger = logger;
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
    }
}
