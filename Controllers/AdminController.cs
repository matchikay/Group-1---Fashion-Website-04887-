using LaFlor.Data;
using LaFlor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaFlor.Controllers
{
    public class AdminController : Controller
    {
        private readonly CustomerContext customerContext;
        public AdminController(CustomerContext _context) {
            customerContext = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders() => View( await customerContext.Orders.Where(m=>m.order_status == "Pending").ToListAsync());

        public IActionResult UpdateOrder(int id, string status)
        {
            var orders =  customerContext.Orders.Where(m => m.order_id == id).FirstOrDefault();
            if (orders != null)
            {
                orders.order_status = status;
                customerContext.Update(orders);
                customerContext.SaveChanges();
               
            }

            return RedirectToAction("Orders", "Admin");
        }
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await customerContext.Orders.Where(m => m.order_id == id).FirstOrDefaultAsync();
            if (order != null)
            {
                customerContext.Orders.Remove(order);
                await customerContext.SaveChangesAsync();
            }

            return RedirectToAction("Orders", "Admin");
        }


        public async Task<IActionResult> CompletedOrders()
        {
            return View(await customerContext.Customer
                     .Join(
                         customerContext.Orders,
                         customer => customer.customer_id,
                         order => order.customer_id,
                         (customer, order) => new CartWithCustomer
                         {
                             customer = customer,
                             orders = order
                         }).Where(orders=>orders.orders.order_status == "Completed")
                     .ToListAsync());
        }
    }
}
