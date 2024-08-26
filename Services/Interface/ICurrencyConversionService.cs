using System.Threading.Tasks;

namespace WalletServiceAPI.Services.Interfaces
{
    public interface ICurrencyConversionService
    {
        Task<decimal> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency);
    }
}
