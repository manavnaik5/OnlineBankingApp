using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Models;
using OnlineBankingApp.ViewModels;

namespace OnlineBankingApp.Controllers
{
    [Authorize]
    public class BankingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public BankingController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);

            if (account == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var viewModel = new BankingViewModel
            {
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                FullName = user.FullName
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(DepositViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);

                if (account != null && model.Amount > 0)
                {
                    account.Balance += model.Amount;

                    var transaction = new Transaction
                    {
                        FromAccountId = account.Id,
                        Amount = model.Amount,
                        Type = TransactionType.Deposit,
                        Description = "Deposit",
                        TransactionDate = DateTime.Now
                    };

                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Deposit successful!";
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(WithdrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);

                if (account != null && model.Amount > 0 && account.Balance >= model.Amount)
                {
                    account.Balance -= model.Amount;

                    var transaction = new Transaction
                    {
                        FromAccountId = account.Id,
                        Amount = model.Amount,
                        Type = TransactionType.Withdrawal,
                        Description = "Withdrawal",
                        TransactionDate = DateTime.Now
                    };

                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Withdrawal successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Insufficient funds or invalid amount.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var fromAccount = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);
                var recipientUser = await _userManager.FindByEmailAsync(model.RecipientEmail);
                var toAccount = recipientUser != null ? _context.Accounts.FirstOrDefault(a => a.UserId == recipientUser.Id) : null;

                if (fromAccount != null && toAccount != null && fromAccount.Id != toAccount.Id && fromAccount.Balance >= model.Amount && model.Amount > 0)
                {
                    fromAccount.Balance -= model.Amount;
                    toAccount.Balance += model.Amount;

                    var transaction = new Transaction
                    {
                        FromAccountId = fromAccount.Id,
                        ToAccountId = toAccount.Id,
                        Amount = model.Amount,
                        Type = TransactionType.Transfer,
                        Description = $"Transfer to {recipientUser.Email}",
                        TransactionDate = DateTime.Now
                    };

                    _context.Transactions.Add(transaction);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Transfer successful!";
                    return RedirectToAction("Index");
                }
                else if (toAccount == null)
                {
                    ModelState.AddModelError("", "Recipient email not found or account inactive.");
                }
                else if (fromAccount.Id == toAccount.Id)
                {
                    ModelState.AddModelError("", "You cannot transfer to your own account.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid transfer details or insufficient funds.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Transactions()
        {
            var user = await _userManager.GetUserAsync(User);
            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);

            if (account == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var transactions = _context.Transactions
                .Where(t => t.FromAccountId == account.Id)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();

            return View(transactions);
        }
    }
}
