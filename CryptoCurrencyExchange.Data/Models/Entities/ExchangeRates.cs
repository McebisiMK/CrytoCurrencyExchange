using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoCurrencyExchange.Data.Models.Entities
{
    public class ExchangeRates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 1)]
        public int Id { get; set; }
        public string Currency { get; set; }
        public Rates Rates { get; set; }
    }
}