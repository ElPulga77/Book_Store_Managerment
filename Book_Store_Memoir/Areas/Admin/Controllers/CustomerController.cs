using Book_Store_Memoir.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomerController (ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            var user = _db.Customers.Include(p=>p.Orders).ToList();
            return View(user);
        }
        public IActionResult OrderList(int id)
        {
            var order = _db.Orders.Include(p => p.Customers).Include(p => p.OrderStatus).Where(p=>p.CustomerId == id);
            return View(order);
        }
    }
}
