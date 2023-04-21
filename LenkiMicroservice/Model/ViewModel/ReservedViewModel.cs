using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Model.ViewModel
{
    public class ReservedViewModel
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
        public int BookId { get; set; }
    }

    public class Books

    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string ISBNNo { get; set; }
        public string Description { get; set; }
        public bool Reserved { get; set; }
        public bool Borrowed { get; set; }
    }


    public class BookReservedViewModel
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string ISBNNo { get; set; }
        public string Description { get; set; }
        public bool Reserved { get; set; }
        public bool Borrowed { get; set; }
    }

}
