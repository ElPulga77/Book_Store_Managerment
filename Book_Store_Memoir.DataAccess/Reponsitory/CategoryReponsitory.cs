using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.DataAccess.Reponsitory
{
    public class CategoryReponsitory 
    {
        public ApplicationDbContext _db;
        public CategoryReponsitory(ApplicationDbContext db) 
        {
            _db = db;
        }
        public List<Category> ListCate()
        {
            var category = _db.Categories.ToList();
            return category;
        }    
        public void AddCate(Category category) 
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }
        public void DeleteCate(int id) {
            // Tìm danh mục cần xóa dựa trên ID
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {    
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }
}
