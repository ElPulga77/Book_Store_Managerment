using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Book_Store_Memoir.Models.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Book_Store_Memoir.DataAccess.Reponsitory
{
    public class BookReponsitory
    {
        public ApplicationDbContext _db;
        private readonly string _connectionString;
        private readonly string _searchProduct = "EXEC FilterProduct";
        private readonly string _searchStringProduct = "EXEC SearchProducts";
        private readonly ApplicationDbContext _dbContext;
        public BookReponsitory(ApplicationDbContext db, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _db = db;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbContext = dbContext;
        }

        public List<Book> ListBook()
        {
            var book = _db.Books.Include(p => p.Publisher);
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
        public List<Book> SearchProduct(int categoryId, int publisherId, int languageId)
        {
            var query = _dbContext.Set<Book>().FromSqlRaw(
                $"{_searchProduct} @categoryId, @publisherId, @languageId" ,
                new SqlParameter("categoryId", categoryId),
                new SqlParameter("publisherId", publisherId),
                new SqlParameter("languageId", languageId)
).ToList();


            var books = query.ToList();

            return books;
        }
        public List<Book> SearchProduct(string Search)
        {
            var product = _db.Set<Book>()
                .FromSqlRaw($"{_searchProduct}  @ProductName",
                    new SqlParameter("ProductName", Search)
                    ) // Thay 123 bằng giá trị thích hợp cho CategoryId
                .ToList();

            return product;
        }
        public List<Book> SearchProduct1(string Search)
        {
            var product = _db.Set<Book>()
                .FromSqlRaw($"{_searchStringProduct}  @ProductName",
                    new SqlParameter("ProductName", Search)
                    ) // Thay 123 bằng giá trị thích hợp cho CategoryId
                .ToList();

            return product;
        }
        /* public List<Book> SearchProductById(int categoryId, int publisherId, int languageId)
         {
             List<Book> result = new List<Book>();

             using (SqlConnection connection = new SqlConnection("DefaultConnection"))
             {
                 using (SqlCommand command = new SqlCommand(_searchProduct, connection))
                 {
                     command.CommandType = CommandType.StoredProcedure;

                     // Thêm tham số vào procedure
                     command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int) { Value = categoryId });
                     command.Parameters.Add(new SqlParameter("@publisherId", SqlDbType.Int) { Value = publisherId });
                     command.Parameters.Add(new SqlParameter("@languageId", SqlDbType.Int) { Value = languageId });

                     connection.Open();

                     using (SqlDataReader reader = command.ExecuteReader())
                     {
                         result = reader.Cast<IDataRecord>()
                             .Select(record => new Book
                             {
                                 // Set các thuộc tính của Book từ record
                                 // Ví dụ:
                                 // Id = record.GetInt32(record.GetOrdinal("Id")),
                                 // Title = record.GetString(record.GetOrdinal("Title")),
                                 // ...
                                 Id = record.GetInt32(record.GetOrdinal("Id")),
                                 Title = record.GetString(record.GetOrdinal("Title"))
                             })
                             .ToList();
                     }
                 }
             }
             return result;
         }*/
        public List<Book> SearchProductById(int categoryId, int publisherId, int languageId)
        {

            return _db.Books.FromSqlRaw("EXEC FilterProduct @categoryId, @publisherId, @languageId",
            new SqlParameter("@categoryId", categoryId),
            new SqlParameter("@publisherId", publisherId),
            new SqlParameter("@languageId", languageId)).ToList();
        }

    }
}
