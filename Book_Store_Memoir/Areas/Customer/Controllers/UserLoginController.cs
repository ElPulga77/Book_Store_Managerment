﻿using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserLoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserLoginController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Customers customer, string password, string name)
        {
            var f_password = ComputeMd5Hash(password);
            var data = _db.Customers.Where(s => s.Name.Equals(name) && s.Password.Equals(f_password)).ToList();
            if (data.Count() > 0)
            {
                HttpContext.Session.SetString("Name", data.FirstOrDefault().Name);
                HttpContext.Session.SetInt32("idUser", data.FirstOrDefault().CustomerId);
                ViewBag.Name = data.FirstOrDefault().Name;
                return RedirectToAction("Index", "Home", new { name = data.FirstOrDefault().Name });
            }
            return View();
        }
        public static string ComputeMd5Hash(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customers user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Customers.FirstOrDefault(s => s.Name == user.Name);
                if (check == null)
                {
                    user.Password = ComputeMd5Hash(user.Password);

                    _db.Customers.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login1()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login1(Customers customer, string password)
        {
            var f_password = ComputeMd5Hash(password);
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var user = _db.Customers.Where(x=>x.Name.Equals(customer.Name)&& x.Password.Equals(customer.Password)).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.SetString("UserName", user.Name.ToString());
                    HttpContext.Session.SetString("Phone", user.Phone.ToString());
                    HttpContext.Session.SetObject("User", user);
                    return RedirectToAction("Index", "Home");
                }    
            }
            return View();
        }
    }
}