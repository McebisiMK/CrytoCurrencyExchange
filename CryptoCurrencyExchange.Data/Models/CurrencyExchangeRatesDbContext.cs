using CryptoCurrencyExchange.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoCurrencyExchange.Data.Models
{
    public class CurrencyExchangeRatesDbContext : DbContext
    {
        public CurrencyExchangeRatesDbContext()
        {
        }

        public CurrencyExchangeRatesDbContext(DbContextOptions<CurrencyExchangeRatesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }
        public virtual DbSet<Rates> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRates>().ToTable("ExchangeRates");
            modelBuilder.Entity<Rates>().ToTable("Rates");
        }
    }
}