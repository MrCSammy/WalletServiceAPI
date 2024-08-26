using System;
using System.Net.Http;
using System.Threading.Tasks;
using WalletServiceAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace WalletServiceAPI.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CurrencyConversionService> _logger;

        public CurrencyConversionService(HttpClient httpClient, ILogger<CurrencyConversionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            var conversionRate = await GetConversionRateAsync(fromCurrency, toCurrency);
            return amount * conversionRate;
        }

        private async Task<decimal> GetConversionRateAsync(string fromCurrency, string toCurrency)
        {
            try
            {
                // Example: Replace with actual API call to get conversion rate
                // For the sake of example, returning a static conversion rate
                var conversionRate = 1.2m; // Static value; replace with actual logic
                return conversionRate;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching conversion rate from {fromCurrency} to {toCurrency}: {ex.Message}");
                throw;
            }
        }
    }
}
