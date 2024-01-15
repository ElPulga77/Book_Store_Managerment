using AspNetCoreHero.ToastNotification.Abstractions;
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
        public INotyfService _notyfService { get; }
        public OrderController(ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;
            _notyfService = notyfService;
        }
        public IActionResult Index(int statusID)
        {
            ViewBag.DSTT = new SelectList(_db.OrderStatus.ToList(), "Id", "Status");

            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            IEnumerable<Orders> items = _db.Orders.Include(p => p.OrderStatus);



            if (statusID != 0)
            {
                items = _db.Orders.Include(p=>p.OrderStatus).Include(p=>p.Customers).AsNoTracking().Where(x => x.OrderStatusId == statusID);
            }


            return View(items);
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
        public IActionResult ConfirmOrder(Orders x, int id )
        {
            Orders hv = _db.Orders.Find(x.Id);
            OrderDetails dt = _db.OrderDetails.FirstOrDefault(x => x.OrdersId == id);
            Book book = _db.Books.Find(x.BookId);
            if (hv != null)
            {
               
                if (hv.OrderStatusId == 1)
                {
                    hv.OrderStatusId = 2;        
                    _db.Orders.Update(hv);
                    _db.SaveChanges();
                    List<OrderDetails> orderDetailsList = _db.OrderDetails.Where(od => od.OrdersId == id).ToList();
                    foreach (var orderDetail in orderDetailsList)
                    {
                        Book bookToUpdate = _db.Books.Find(orderDetail.BookId);
                        if (bookToUpdate != null)
                        {
                            // Giảm Quantity tương ứng trong Book
                            bookToUpdate.Quantity -= orderDetail.Quantity;

                            // Cập nhật lại trong cơ sở dữ liệu
                            _db.Books.Update(bookToUpdate);
                        }
                    }
                    _db.SaveChanges();
                    _notyfService.Success("Đơn hàng đã được xác nhận!!!");
                    return RedirectToAction("Index");
                }
                else if (hv.OrderStatusId == 5)
                {
                    _notyfService.Error("Đơn hàng này đã bị hủy trước đó!!!");
                }
                else if (hv.OrderStatusId == 2)
                {
                    _notyfService.Error("Đơn hàng này đã được xác nhận trước đó trước đó!!!");
                }
                else if (hv.OrderStatusId == 3)
                {
                    _notyfService.Error("Đơn hàng này đang được vận chuyển!!!");
                }
                else if (hv.OrderStatusId == 5)
                {
                    _notyfService.Error("Đơn hàng này đã bị hủy trước đó!!!");
                }
               else
                {
                    _notyfService.Error("Đơn hàng này đã được giao!!!");

                }
                /* _notyfService.Success("Thay đổi trạng thái thành công");*/
            }
            return RedirectToAction("Details", new { id });
        }
        public IActionResult CancelOrder(Orders x, int id)
        {
            Orders hv = _db.Orders.Find(x.Id);
            if (hv != null)
            {
                hv.OrderStatusId = 5;
                _db.Orders.Update(hv);
                _db.SaveChanges();
                _notyfService.Information("Đơn hàng đã bị hủy!!!");
                return RedirectToAction("Details", new { id });
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
                if (orderDetail.Quantity > 1)
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
        public IActionResult CreateDelivery(DeliveryReceipt receipt, ReceiptDetails details)
        {
            bool isDeliveryReceiptExists = _db.DeliveryReceipts.Any(dr => dr.OrderId == receipt.Id);
            if (isDeliveryReceiptExists)
            {

                _notyfService.Error("Phiếu giao hàng đã được tạo trước đó!!!!");
                return RedirectToAction("Index");
            }

            DeliveryReceipt deliveryReceipt = new DeliveryReceipt();
            deliveryReceipt.DeliveryDate = DateTime.Now;
            deliveryReceipt.CreatedAt = DateTime.Now;
            deliveryReceipt.OrderId = receipt.Id;
            deliveryReceipt.ShipperId = 1;
            deliveryReceipt.TotalAmount = receipt.TotalAmount;
            _db.Add(deliveryReceipt);
            _db.SaveChanges();
            _notyfService.Success("Tạo thành công phiếu giao hàng!!!!");
            return RedirectToAction("Index");
        }
    }
}
