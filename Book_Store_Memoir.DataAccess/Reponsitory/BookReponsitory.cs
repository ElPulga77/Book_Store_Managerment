using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.DataAccess.Reponsitory
{
    public class BookReponsitory 
    {
        public ApplicationDbContext _db;
        
        public BookReponsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Book> ListBook()
        {
            var book = _db.Books.Include(p=>p.Publisher);
            return book.ToList();
        }
        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
        public void DeleteBook(int id)
        {         
            var book = _db.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
            }
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
    }
}
