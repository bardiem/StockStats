using Microsoft.EntityFrameworkCore;
using StockStats.Domain.Entities;

namespace StockStats.Data
{
    public class StockStatsDbContext : DbContext
    {
        public StockStatsDbContext(DbContextOptions<StockStatsDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-N2527SN;Initial Catalog=StocksDB;Integrated Security=True;TrustServerCertificate=True;");
        //}

        public DbSet<Symbol> Symbols { get; set; }

        public DbSet<SymbolPerformance> SymbolPerformance { get; set; }
    }
}