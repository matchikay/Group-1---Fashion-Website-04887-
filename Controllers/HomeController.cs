using LaFlor.Data;
using LaFlor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LaFlor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CustomerContext _context1;

        public HomeController(ILogger<HomeController> logger, CustomerContext _context)
        {
            _logger = logger;
            _context1 = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterButton(string? username,string email, string password)
        {
            Customer customer = new Customer { customer_username = username, customer_email = email, customer_password = password };
            _context1.Customer.Add(customer);
            _context1.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful. You can now login with your credentials.";

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LoginButton(string? email, string password)
        {
            if(email.CompareTo("admin@gmail.com")==0 && password.CompareTo("admin") == 0)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var customer = await _context1.Customer.FirstOrDefaultAsync(m => m.customer_email == email && m.customer_password == password);
                if (customer == null)
                {
                    TempData["invalidLogin"] = "Incorrect email and password.";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("name", customer.customer_username);
                    HttpContext.Session.SetInt32("id", (int) customer.customer_id);
                    return RedirectToAction("Index", "User");
                }

            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
