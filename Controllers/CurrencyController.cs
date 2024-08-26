using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServiceAPI.Services.Interfaces;

namespace WalletServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyConversionService _currencyConversionService;

        public CurrencyController(ICurrencyConversionService currencyConversionService)
        {
            _currencyConversionService = currencyConversionService;
        }

        [HttpGet("convert")]
        public async Task<IActionResult> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            var convertedAmount = await _currencyConversionService.ConvertCurrencyAsync(amount, fromCurrency, toCurrency);
            return Ok(convertedAmount);
        }
    }
}
