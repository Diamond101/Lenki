using LenkiMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Interface
{
   public interface IBorrow
    {
        IEnumerable<BorrowBooks> ListBoorwedBooks(int customerid);
        void BorrowBook(BorrowBook borrowBooks);
        void DeleteBorow(int bookIdId);
        void ReturnBook(BorrowBooks borrow);
        Books GetBookByID(int bookId);
        Notifications CheckNotification(int bookId);
        Users GetCustomerByID(int customersId);

        void Save();
    }
}
