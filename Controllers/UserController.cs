using LaFlor.Data;
using LaFlor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaFlor.Controllers
{
    public class UserController : Controller
    {
        private readonly CustomerContext _customerContext;
        public UserController(CustomerContext co) {
            _customerContext = co;
        }
        public IActionResult Index()
        {
         
            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.id = HttpContext.Session.GetInt32("id");
            return View();
        }
        public async Task<IActionResult> Orders() => View( await _customerContext.Orders.Where(m=>m.customer_id == HttpContext.Session.GetInt32("id")).ToListAsync());
        public IActionResult Profile() => View();
        public async Task<IActionResult> Cart()
        {
            ViewBag.grandTotal = _customerContext.Cart.Sum(cart=>cart.cart_total);
            var cart = _customerContext.Cart.Where(flower => flower.customer_id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
            if (cart == null)
            {

            }
            else
            {
                return View(await _customerContext.Flowers
                    .Join(
                        _customerContext.Cart,
                        flowers => flowers.flower_id,
                        cart => cart.flower_id,
                        (flowers, cart) => new CartWithCustomer
                        {
                            flowers = flowers,
                            cart = cart
                        })
                    .ToListAsync());
            }
           return View();
        }
        public IActionResult Checkout(int total)
        {
            HttpContext.Session.SetInt32("grand_total", total);
            return View();
        }
        public async Task<IActionResult> CheckoutOrder(string name, string number,string email, string payment, string address1, string address2, string barangay, string city, string province,string zip )
        {
            Orders order = new Orders { order_name = name, order_number = number, order_email = email, order_payment_method = payment, order_address1 = address1, order_address2 = address2, order_barangay = barangay, order_city = city, order_province = province, order_zip = zip, order_placed_date = Convert.ToString(DateTime.Now), order_total = HttpContext.Session.GetInt32("grand_total"), order_status = "Pending" , customer_id = HttpContext.Session.GetInt32("id") };
            _customerContext.Orders.Add(order);
            _customerContext.SaveChanges();

            int? customerId = HttpContext.Session.GetInt32("id");
            if (customerId == null)
            {
                return BadRequest("Customer ID is not set in the session.");
            }

            var cartItems = await _customerContext.Cart
                .Where(m => m.customer_id == customerId)
                .ToListAsync();

            if (cartItems != null && cartItems.Any())
            {
                _customerContext.Cart.RemoveRange(cartItems);
                await _customerContext.SaveChangesAsync();
            }


            return RedirectToAction("Cart");
        }
        public async Task<IActionResult> DeleteCart(int id)
        {

            var cart = await _customerContext.Cart.Where(m=>m.cart_id == id).FirstOrDefaultAsync();
            if (cart != null)
            {
                _customerContext.Cart.Remove(cart);
            }

            await _customerContext.SaveChangesAsync();
            return RedirectToAction("Cart", "User");
        }
        public async Task<IActionResult> UpdateCart(int id,int qty)
        {
       
            var cart = await _customerContext.Cart.Where(m => m.cart_id == id).FirstOrDefaultAsync();
            var flower = await _customerContext.Flowers.Where(m => m.flower_id == cart.flower_id).FirstOrDefaultAsync();
            if (cart != null)
            {
                cart.cart_qty = qty;
                cart.cart_total = (int)flower.flower_price * qty;
                _customerContext.Cart.Update(cart);
            }

            await _customerContext.SaveChangesAsync();
            return RedirectToAction("Cart", "User");
        }


        public async Task<IActionResult> Shop() => View(await _customerContext.Flowers.ToListAsync());

        public async Task<IActionResult> AddCart(int id, int qty)
        {
            var flowers = _customerContext.Flowers.Where(m=>m.flower_id == id).FirstOrDefault();

            Cart cart = new Cart { cart_qty = qty, cart_total = (int)(flowers.flower_price * qty), cart_status = "Pending", flower_id = id, customer_id = (int) HttpContext.Session.GetInt32("id") };
            _customerContext.Cart.Add(cart);
            _customerContext.SaveChanges();

            return RedirectToAction("Shop");
        }
    }
}
