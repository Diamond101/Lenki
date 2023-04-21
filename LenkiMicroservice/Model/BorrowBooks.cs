using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Model
{
    public class BorrowBooks
    {
        [Key]
        public int BorrowedId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class BorrowBook
    {
        
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
