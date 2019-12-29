using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishbuy.Data
{
    public class QRCodeContext : DbContext
    {

        public QRCodeContext(DbContextOptions<QRCodeContext> options)
            : base(options)
        {
        }

        public DbSet<QRToken> QRTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

    public partial class QRToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
    }

}
