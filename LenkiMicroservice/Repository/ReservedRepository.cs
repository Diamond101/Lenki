using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using LenkiMicroservice.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Repository
{
    public class ReservedRepository : IReserved
    {
        private readonly LenkiDBContext _dbContext;

        public ReservedRepository(LenkiDBContext dbContext)
        {
           this. _dbContext = dbContext;
            //this.tdbc = tdbc;
        }
        public void DeleteReseved(int reservedId)
        {
            var res = _dbContext.ReservedBooks.Find(reservedId);
            _dbContext.ReservedBooks.Remove(res);
            Save();
        }

        public Model.Books GetBookByID(int bookId)
        {
            return _dbContext.Books.Find(bookId);
        }

        public void ReservedBook(ReservedBooks customer)
        {
            _dbContext.Add(customer);
            Save();
        }

        public IEnumerable<ReservedViewModel> ReservedBookscustomers(int customerid)
        {
            if (customerid != null)
            {
                var result = (from users in this._dbContext.Users
                              join Res in this._dbContext.ReservedBooks
                              on users.Id equals Res.CustomerId
                              where Res.CustomerId ==customerid
                              select new ReservedViewModel
                              {
                                  FullName=users.FullName,
                                  Email=users.Email,
                                  Phone=users.Phone, 
                                  BookId=Res.BookId,
                              }).ToList();

                return result;
            }
            else
            {
                var result = (from users in this._dbContext.Users
                              join Res in this._dbContext.ReservedBooks
                              on users.Id equals Res.CustomerId
                              select new ReservedViewModel
                              {
                                  FullName = users.FullName,
                                  Email = users.Email,
                                  Phone = users.Phone,
                                  BookId = Res.BookId,
                              }).ToList();
                return result;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateReserved(ReservedBooks customer)
        {
            var currBook = GetBookByID(customer.BookId);
            currBook.Reserved = false;
            _dbContext.Entry(currBook).State = EntityState.Modified;
            _dbContext.Entry(customer).State = EntityState.Modified;
            Save();
        }
    }
}


 