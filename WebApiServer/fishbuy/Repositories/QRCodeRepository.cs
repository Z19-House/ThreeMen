using fishbuy.Data;
using fishbuy.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public class QRCodeRepository : IQRCodeRepository
    {
        private readonly QRCodeContext _context;
        private readonly IAuthService _service;

        public QRCodeRepository(QRCodeContext context, IAuthService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<QRToken> Generate()
        {
            var item = await _context.QRTokens.AddAsync(new QRToken
            {
                Id = Guid.NewGuid(),
                Token = _service.GenerateRefreshToken(),
                Status = 0,
                Expires = DateTime.UtcNow.AddMinutes(1)
            });
            await _context.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<QRToken> PermitByToken(string token, int uid)
        {
            var item = await _context.QRTokens.FirstOrDefaultAsync(it => it.Token == token);
            if (item == null || item.Expires < DateTime.UtcNow)
            {
                return null;
            }
            item.Status = 1;
            item.UserId = uid;
            _context.QRTokens.Update(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<QRToken> GetById(Guid id)
        {
            return await _context.QRTokens.FirstOrDefaultAsync(it => it.Id == id);
        }

        public void GetLatest(ref QRToken token)
        {
            _context.Entry(token).Reload();
        }

    }
}
