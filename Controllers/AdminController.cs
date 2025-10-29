using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);
            var transactions = new List<Transaction>();
            if (account != null)
            {
                transactions = _context.Transactions.Where(t => t.FromAccountId == account.Id).ToList();
            }

            ViewBag.Account = account;
            ViewBag.Transactions = transactions;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user.Id);
            if (account != null)
            {
                account.IsActive = !account.IsActive;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("UserDetails", new { id });
        }

        public IActionResult Transactions()
        {
            var transactions = _context.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .ToList();

            return View(transactions);
        }
    }
}
