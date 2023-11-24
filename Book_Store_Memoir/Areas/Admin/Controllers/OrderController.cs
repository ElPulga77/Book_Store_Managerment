using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var order = _db.Orders.Include(p => p.Customers).Include(p=>p.OrderStatus);
            return View(order.ToList());
        }
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.DSSP = new SelectList(_db.Shipper.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var order = await _db.Orders
                .Include(o => o.Customers).Include(o=>o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var Chitietdonhang = _db.OrderDetails
                .Include(x => x.Book)
                .Where(x => x.OrdersId == id)
                .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();

            return View(order);
        }
        /*public async Task<IActionResult> ConfirmOrder(Orders orders, int? id)
        {
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

            // Đặt OrderStatusId thành 2 (hoặc giá trị mới bạn muốn)
            orders.OrderStatusId = 2;

            // Lưu thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            // Redirect hoặc trả về kết quả
            return RedirectToAction("Details", new { id = order.Id });
        }*/
        public IActionResult ConfirmOrder(Orders x)
        {         
                Orders hv = _db.Orders.Find(x.Id);
                if (hv != null)
                {
                    hv.OrderStatusId = 2;                   
                    _db.Orders.Update(hv);
                    _db.SaveChanges();
                    /* _notyfService.Success("Thay đổi trạng thái thành công");*/
                }
                return RedirectToAction("Index");
        }
        public IActionResult ListOrderConfirm(int statusId = 2)
        {
            var dsordercf = _db.Orders.Include(p => p.Customers).Include(p => p.OrderStatus).Where(order => order.OrderStatusId == 2).ToList();
            return View(dsordercf);  
        }
        public async Task<IActionResult> View(int? id)
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

            var Chitietdonhang = _db.OrderDetails
                .Include(x => x.Book)
                .Where(x => x.OrdersId == id)
                .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();

            return View(order);
        }
        public IActionResult Minus(int orderDetailId, int id)
        {
            var orderDetail = _db.OrderDetails.Find(orderDetailId);
            if (orderDetail != null)
            {
              if(orderDetail.Quantity > 1)
                {
                    orderDetail.Quantity--;
                    _db.SaveChanges();
                }    
            }
            return RedirectToAction("Details", new { id });
        }
        public IActionResult Plus(int orderDetailId, int id)
        {
            var orderDetail = _db.OrderDetails.Find(orderDetailId);
            if (orderDetail != null)
            {  
                    orderDetail.Quantity++;
                    _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }
        public IActionResult CreateDelivery (DeliveryReceipt receipt, ReceiptDetails details)
        {
            DeliveryReceipt deliveryReceipt = new DeliveryReceipt();    
            deliveryReceipt.DeliveryDate = DateTime.Now;
            deliveryReceipt.CreatedAt = DateTime.Now;
            deliveryReceipt.OrderId = receipt.Id;
            deliveryReceipt.ShipperId = 1;
            _db.Add(deliveryReceipt);
            _db.SaveChanges();
            return RedirectToAction("Index");  
        }
    }
}
