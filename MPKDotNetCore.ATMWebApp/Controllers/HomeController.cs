using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPKDotNetCore.ATMWebApp.EFDbContext;
using MPKDotNetCore.ATMWebApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MPKDotNetCore.ATMWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");

            if (serializedAccount != null)
            {
                Account loggedInAccount = JsonConvert.DeserializeObject<Account>(serializedAccount);
                return View(loggedInAccount);
            }
            else
            {
                return View(new Account());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account reqmodel)
        {
            Random random = new Random();
            string accountNo = "";
            for (int i = 0; i < 7; i++)
            {
                accountNo += random.Next(1, 9);
            }
            reqmodel.AccountNo = int.Parse(accountNo);
            await _context.Accounts.AddAsync(reqmodel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful. Your account no is " + reqmodel.AccountNo : "Saving Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Account reqmodel)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNo == reqmodel.AccountNo && x.Pin == reqmodel.Pin);
            if (account is null)
            {
                MessageModel model = new MessageModel(false, "Login failed!");
                return Json(model);
            }
            else
            {
                HttpContext.Session.SetString("LoggedInAccount", JsonConvert.SerializeObject(account));
                MessageModel model = new MessageModel(true, "Login success!");
                return Json(model);
            }
        }

        public IActionResult SignOut()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CheckBalance()
        {
            string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
            if (serializedAccount == null)
            {
                MessageModel model = new MessageModel(false, "Something went wrong");
                return Json(model);
            }


            else
            {
                Account loggedInAccount = JsonConvert.DeserializeObject<Account>(serializedAccount);
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == loggedInAccount.Id);
                MessageModel model = new MessageModel(true, "Your current balance is " + account.Balance);
                return Json(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(decimal amount)
        {
            string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
            if (serializedAccount == null)
            {
                MessageModel model = new MessageModel(false, "Something went wrong");
                return Json(model);
            }


            else
            {
                Account loggedInAccount = JsonConvert.DeserializeObject<Account>(serializedAccount);
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == loggedInAccount.Id);
                account.Balance += amount;
                _context.Accounts.Update(account);
                var result = await _context.SaveChangesAsync();

                string message = result > 0 ? "Deposit success. Your current balance is " + account.Balance : "Deposit Failed.";

                MessageModel model = new MessageModel(result > 0, message);
                return Json(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(decimal amount)
        {
            string serializedAccount = HttpContext.Session.GetString("LoggedInAccount");
            if (serializedAccount == null)
            {
                MessageModel model = new MessageModel(false, "Something went wrong");
                return Json(model);
            }


            else
            {
                Account loggedInAccount = JsonConvert.DeserializeObject<Account>(serializedAccount);
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == loggedInAccount.Id);

                if (account.Balance < amount)
                {
                    MessageModel model = new MessageModel(false, "You don't have enough balance");
                    return Json(model);
                }

                else
                {
                    account.Balance -= amount;
                    _context.Accounts.Update(account);
                    var result = await _context.SaveChangesAsync();

                    string message = result > 0 ? "Withdraw success. Your current balance is " + account.Balance : "Deposit Failed.";

                    MessageModel model = new MessageModel(result > 0, message);
                    return Json(model);
                }


            }
        }


    }
}