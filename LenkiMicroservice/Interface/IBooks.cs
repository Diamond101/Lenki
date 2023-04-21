using LenkiMicroservice.Model;
using System.Collections.Generic;

namespace LenkiData.Interface
{
    public  interface IBooks
    {
        IEnumerable<Books> GetBooks();
        Books GetBookByID(int books);
        void InsertBook(Book books);
        void DeleteBook(int bookId);
        void UpdateBook(Books books);
        void Save();
    }
}
