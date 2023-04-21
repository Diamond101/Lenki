using LenkiMicroservice.Model;
using System.Collections.Generic;

namespace LenkiData.Interface
{
    public  interface IBooks
    {
        IEnumerable<Books> GetBooks( string BookName);
        Books GetBookByID(int books);
        void InsertBook(Book books);
        void SetNotification(Notification notification);
        void DeleteBook(int bookId);
        void UpdateBook(Books books);
        void Save();
    }
}
