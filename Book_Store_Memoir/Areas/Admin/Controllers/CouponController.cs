using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public CouponController(ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var coupon = _db.Coupons.ToList();
            return View(coupon);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Coupon coupon)
        {
            if(ModelState.IsValid)
            {
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _notyfService.Success("Thêm mã khuyến mãi thành công!!");

            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string id)
        {
            int dem = _db.Orders.Where(a => a.CouponId == id).ToList().Count;
            ViewBag.flag = dem;
            if (dem > 0)
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(x => x.Id == id);
                _notyfService.Error("Không thể xóa thể loại này!!!!");
                return View(coupon);
            }
            else
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(x => x.Id == id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _notyfService.Success("Mã khuyến mãi đã bị xóa!!");
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(string id)
        {
            var cat = _db.Coupons.FirstOrDefault(x => x.Id == id);
            return View(cat);
        }
        [HttpPost]
        public IActionResult Edit(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _notyfService.Success("Cập nhật thông tin mã khuyến mãi thành công!!!!");

            }
            return RedirectToAction("Index");
        }
    }
}
