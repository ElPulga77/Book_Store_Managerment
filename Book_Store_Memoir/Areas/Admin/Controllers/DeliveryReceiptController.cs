using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
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
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var receipt = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(pa=>pa.Customers).Include(p => p.Shipper).ToList();
            return View(receipt);

        }
        public IActionResult Detail(int id, int idOrders)
        {   
            ViewBag.DSSP = new SelectList(_db.Shipper.ToList(), "Id", "Name");
            var receipt = _db.DeliveryReceipts.Include(p=>p.Orders).ThenInclude(pa=>pa.Customers).Include(p=>p.Shipper).FirstOrDefault(m=>m.Id == id);
            var Chitietdonhang = _db.OrderDetails
               .Include(x => x.Book)
               .Where(x => x.OrdersId == idOrders)
               .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();
            return View(receipt);
        }
        [HttpPost]
        public IActionResult CreateReceipt(ReceiptDetails receipt, int idOrders, int idDeli, int ShipperID)
        {
            var items = _db.DeliveryReceipts.Find(idDeli);
            items.ShipperId = ShipperID;
            _db.Update(items);
            _db.SaveChanges();
            var order = _db.Orders.Find(idOrders);
            order.OrderStatusId = 3;
            _db.Update(order);
            _db.SaveChanges();  
            var Chitietdonhang = _db.OrderDetails
               .Include(x => x.Book)
               .Where(x => x.OrdersId == idOrders)
               .OrderBy(x => x.Id);
            foreach (var rece in Chitietdonhang)
            {
                var donhang = new ReceiptDetails
                {
                    BookId = rece.BookId,
                    DeliveryReceiptId = idDeli,
                    Quantity = rece.Quantity,
                    TotalAmount = rece.TotalAmount,
                };
                _db.ReceiptDetails.Add(donhang);
                _db.SaveChanges();
                
            }    
                return RedirectToAction("Index");
        }
        public IActionResult ListReceipt()
        {
            var receipt = _db.ReceiptDetails.Include(x => x.Book).Include(x=>x.DeliveryReceipt).ThenInclude(x => x.Shipper).ThenInclude(p=>p.Orders).ToList();  
            return View();
        }
        public IActionResult EditReceipt(int id)
        {

            var receipt = _db.DeliveryReceipts.Include(p => p.Orders)
                .ThenInclude(o=>o.Customers)
                        .ThenInclude(o=>o.Orders).ThenInclude(e=>e.OrderStatus)
                .Include(p => p.Shipper)
                .FirstOrDefault(m => m.Id == id);
            if (receipt == null)
            {
                return NotFound();
            }
            
            var Chitietdonhang = _db.ReceiptDetails
                .Include(x => x.Book)
                .Where(x => x.DeliveryReceiptId == id)
                .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();
            decimal totalAmount = 0;
            foreach (var item in Chitietdonhang)
            {
                totalAmount += (decimal)(item.Book.Price * item.Quantity);
            }
            if (Chitietdonhang.Any())
            {
                Chitietdonhang.First().TotalAmount = (double)totalAmount;
            }
            return View(receipt);
        }
        public IActionResult Minus(int receiptDetailsId, int id, double totalupdate, ReceiptDetails receipt)
        {
            var orderDetail = _db.ReceiptDetails.Find(receiptDetailsId);
            if (orderDetail != null)
            {
                if (orderDetail.Quantity > 0)
                {
                    orderDetail.Quantity--;
                    _db.SaveChanges();
                    var Chitietdonhang = _db.ReceiptDetails
                    .Include(x => x.Book)
                    .Where(x => x.DeliveryReceiptId == id)
                    .OrderBy(x => x.Id);
                    ViewBag.ChiTiet = Chitietdonhang.ToList();
                    decimal totalAmount = 0;
                    foreach (var item in Chitietdonhang)
                    {
                        totalAmount += (decimal)(item.Book.Price * item.Quantity);
                    }
                    if (Chitietdonhang.Any())
                    {
                        Chitietdonhang.First().TotalAmount = (double)totalAmount;
                    }
                    orderDetail.TotalAmount = Chitietdonhang.First().TotalAmount;
                    _db.ReceiptDetails.Update(orderDetail);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("EditReceipt", new { id });
        }
        public IActionResult Plus(int receiptDetailsId, int id)
        {
            var orderDetail = _db.ReceiptDetails.Find(receiptDetailsId);
            if (orderDetail != null)
            {
                orderDetail.Quantity++;
                _db.SaveChanges();
            }
            return RedirectToAction("EditReceipt", new { id });
        }
    }
}
