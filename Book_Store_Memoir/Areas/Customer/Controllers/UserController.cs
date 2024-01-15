using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public UserController (ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;   
            _notyfService = notyfService;   
        }
        public IActionResult Index(int id)
        {
            
            string userName = HttpContext.Session.GetString("UserName");
            var user = HttpContext.Session.GetObject<Customers>("User");
            ViewBag.UserName = userName;
            if (user != null)
            {
                ViewBag.DuyNe = user.CustomerId;
            }
            else
            {
                // Xử lý khi user là null, có thể gán một giá trị mặc định hoặc làm gì đó tương ứng
                ViewBag.DuyNe = "";
            }
            var dh = _db.Orders.Include(p => p.OrderStatus).ThenInclude(a=>a.Orders).ThenInclude(a=>a.DeliveryReceipts).Where(p => p.CustomersCustomerId == id);
            var dh1 = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(a=>a.OrderStatus).Where(x => x.Orders.CustomersCustomerId == id);
            var dh2 = _db.DeliveryReceipts.Include(p=>p.ReceiptDetails).Include(p=>p.Orders).ThenInclude(a => a.OrderStatus).Where(x => x.Orders.CustomersCustomerId == id);
            // Lấy thông tin người dùng từ phiên
/*            var user = HttpContext.Session.GetObject<Customers>("User");
*/            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login1", "UserLogin");
            }
            else

                return View("Index", new CustomerInfo
                {
                    
                    Cusname = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    YourOrder = dh.ToList(),                  
                });
        }
        public IActionResult YourOrder(int id)
        {
            var receipt = _db.Orders.Include(p => p.Customers).Include(p=>p.OrderStatus)               
                .FirstOrDefault(m => m.Id == id);
            var Chitietdonhang = _db.OrderDetails
              .Include(x => x.Book)
              .Where(x => x.OrdersId == id)
              .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();
            return View(receipt);
        }
        public IActionResult CancelOrder( int id)
        {
            try
            {
                Orders hv = _db.Orders.Find(id);
                if (hv != null && hv.OrderStatusId == 1)
                {
                    hv.OrderStatusId = 5;
                    _db.Orders.Update(hv);
                    _db.SaveChanges();
                    _notyfService.Information("Đơn hàng đã bị hủy!!!");
                    return RedirectToAction("Details", new { id });
                }
                else
                {
                    _notyfService.Error("Không thể hủy đơn hàng!!!");
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                string errorMessage = $"Không thể thực hiện lệnh";

                Console.WriteLine($"Không thể thực hiện lệnh");
                return RedirectToAction("Index");
            }
        }
    }
}
