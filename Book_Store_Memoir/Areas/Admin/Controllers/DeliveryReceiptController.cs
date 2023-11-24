using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeliveryReceiptController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DeliveryReceiptController(ApplicationDbContext db) { 
            _db=db;
        }
        public IActionResult Index(int? id)
        {
            var receipt = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(pa=>pa.Customers).Include(p => p.Shipper).ToList();
            return View(receipt);

        }
        public IActionResult Detail(int id, int idOrders)
        {   
            ViewBag.DSSP = new SelectList(_db.Shipper.ToList(), "Id", "Name");
            var receipt = _db.DeliveryReceipts.Include(p=>p.Orders).ThenInclude(pa=>pa.Customers).Include(p=>p.Shipper).FirstOrDefault(m=>m.Id == id);

            /*var Chitietdonhang = _db.DeliveryReceipts
                .Include(p => p.Orders)
                .ThenInclude(pa => pa.OrderDetails)
                .ThenInclude(pa => pa.Book).Where(p => p.OrderId == p.Orders.Id)
                .OrderBy(x => x.Id);*/
            var Chitietdonhang = _db.OrderDetails
               .Include(x => x.Book)
               .Where(x => x.OrdersId == idOrders)
               .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();
            return View(receipt);
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.DSSP = new SelectList(_db.Shipper.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var order = await _db.Orders
                .Include(o => o.Customers).Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            var Chitietdonhang1 = _db.DeliveryReceipts
                .Include(p => p.Orders)
                .ThenInclude(pa => pa.OrderDetails)
                .ThenInclude(pa => pa.Book).Where(p => p.OrderId == p.Orders.Id)
                .OrderBy(x => x.Id).ToList();
            
            var Chitietdonhang = _db.OrderDetails
                .Include(x => x.Book)
                .Where(x => x.OrdersId == id)
                .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();

            return View(order);
        }
    }
}
