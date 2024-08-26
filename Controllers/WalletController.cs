using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletServiceAPI.Services.Interfaces;
using WalletServiceAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WalletServiceAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WalletController(IWalletService walletService, UserManager<ApplicationUser> userManager)
        {
            _walletService = walletService;
            _userManager = userManager;
        }

        [HttpPost("create")] 
        public async Task<IActionResult> CreateWallet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wallet = await _walletService.CreateWalletAsync(userId);
            return Ok(wallet);
        }

        [HttpPost("fund")]
        public async Task<IActionResult> FundWallet(Guid walletId, decimal amount)
        {
            await _walletService.FundWalletAsync(walletId, amount);
            return Ok("Wallet funded successfully");
        }

        [HttpPost("debit")]
        public async Task<IActionResult> DebitWallet(Guid walletId, decimal amount)
        {
            await _walletService.DebitWalletAsync(walletId, amount);
            return Ok("Wallet debited successfully");
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFunds(Guid fromWalletId, Guid toWalletId, decimal amount)
        {
            await _walletService.TransferFundsAsync(fromWalletId, toWalletId, amount);
            return Ok("Transfer successful");
        }

        [HttpGet("{walletId}/transactions")]
        public async Task<IActionResult> GetTransactionHistory(Guid walletId, int pageNumber = 1, int pageSize = 10)
        {
            var transactions = await _walletService.GetTransactionHistoryAsync(walletId, pageNumber, pageSize);
            return Ok(transactions);
        }
    }
}
