using LenkiData.Interface;
using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LenkiData.Repository
{
    public class BooksRepository: IBooks
    {
        private readonly LenkiDBContext _dbContext;

        public BooksRepository(LenkiDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int BookId)
        {
            var product = _dbContext.Books.Find(BookId);
            _dbContext.Books.Remove(product);
            Save();
        }

        public Books GetBookByID(int bookId)
        {
            return _dbContext.Books.Find(bookId);
        }

        public IEnumerable<Books> GetBooks()
        {
            return _dbContext.Books.ToList();
        }

        public void InsertBook(Book books)
        {
            Books book = new Books();
            book.BookAuthor = books.BookAuthor;
            book.BookName = books.BookName;
            book.Borrowed = books.Borrowed;
            book.Description = books.Description;
            book.ISBNNo = books.ISBNNo;
            book.Reserved = books.Reserved;
            _dbContext.Add(book);
            Save();
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Books books)
        {
            _dbContext.Entry(books).State = EntityState.Modified;
            Save();
        }
             
    }
}
