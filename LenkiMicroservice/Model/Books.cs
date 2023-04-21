using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Model
{
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

    public class Book

    {        
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string ISBNNo { get; set; }
        public string Description { get; set; }
        public bool Reserved { get; set; }
        public bool Borrowed { get; set; }
    }
}
