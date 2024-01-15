using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models.Models;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShipperController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public ShipperController(ApplicationDbContext db, INotyfService notyfService)
        {
            _notyfService = notyfService;
            _db = db;
        }
        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetString("Shipper") == null)
            {
                _notyfService.Error("Tài khoản của bạn không có quyền truy cập chức năng này!!!!");
                return RedirectToAction("Index", "AdminLogin");
            }
            var shipper = HttpContext.Session.GetObject<Shipper>("Shipper");
            if (shipper != null)
            {
                ViewBag.Shipper = shipper.Id;
            }
            else
            {
                ViewBag.Shipper = null;
            }
            var dh = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(x => x.OrderStatus).Where(p => p.ShipperId == id);
            return View(dh);
        }
        public IActionResult ConfirmDelivery(DeliveryReceipt x)
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
        public IActionResult Details(int id, int idOrder)
        {
            var receipt = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(pa => pa.Customers).Include(p => p.Shipper).FirstOrDefault(m => m.Id == id);
            var Chitietphieugiao = _db.ReceiptDetails
              .Include(x => x.Book)
              .Where(x => x.DeliveryReceiptId == id)
              .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietphieugiao.ToList();
            return View(receipt);
        }
        public IActionResult ConfirmDeliverys(Orders order, int idOrders)
        {
            Orders hv = _db.Orders.Find(idOrders);
            if (hv != null)
            {
                hv.OrderStatusId = 4;
                _db.Orders.Update(hv);
                _db.SaveChanges();
                /* _notyfService.Success("Thay đổi trạng thái thành công");*/
            }
            return RedirectToAction("Index");
        }
    }
}
