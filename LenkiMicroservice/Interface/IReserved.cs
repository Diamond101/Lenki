using LenkiMicroservice.Model;
using LenkiMicroservice.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books = LenkiMicroservice.Model.Books;

namespace LenkiMicroservice.Interface
{
   public interface IReserved
    {
        IEnumerable<ReservedViewModel> ReservedBookscustomers(int customerid);
        void ReservedBook(ReservedBooks customer);
        void DeleteReseved(int reservedId);
        void UpdateReserved(ReservedBooks customer);
        Books GetBookByID(int bookId);
        void Save();
    }
}
