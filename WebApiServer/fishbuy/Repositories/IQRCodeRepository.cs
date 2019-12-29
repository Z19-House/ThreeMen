using fishbuy.Data;
using fishbuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Repositories
{
    public interface IQRCodeRepository
    {
        Task<QRToken> Generate();
        Task<QRToken> PermitByToken(string token, int uid);
        Task<QRToken> GetById(Guid id);
        void GetLatest(ref QRToken token);
    }
}
