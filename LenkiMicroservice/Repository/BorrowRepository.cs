using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Repository
{
    public class BorrowRepository : IBorrow
    {
        private readonly LenkiDBContext _dbContext;

        public BorrowRepository(LenkiDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void BorrowBook(BorrowBook borrowBooks)
        {
            BorrowBook book = new BorrowBook();
            book.CustomerId = borrowBooks.CustomerId;
            book.BookId = borrowBooks.BookId;
            book.ReturnDate = borrowBooks.ReturnDate;
            _dbContext.Add(book);
            Save();
        }

        public void DeleteBorow(int BookId)
        {
            var book = _dbContext.BorrowBooks.Find(BookId);
            _dbContext.BorrowBooks.Remove(book);
            Save();
        }

        public Books GetBookByID(int bookId)
        {
            return _dbContext.Books.Find(bookId);
        }

        public IEnumerable<BorrowBooks> ListBoorwedBooks(int customerid)
        {
            if (customerid>0)
            {
                return _dbContext.BorrowBooks.Where(u => u.CustomerId==customerid).ToList();
            }
            else
            {
                return _dbContext.BorrowBooks.ToList();
            }
        }

        public void ReturnBook(BorrowBooks borrow)
        {
            var currBook = GetBookByID(borrow.BookId);
            currBook.Borrowed = false;
            _dbContext.Entry(currBook).State = EntityState.Modified;
            _dbContext.Entry(borrow).State = EntityState.Modified;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
