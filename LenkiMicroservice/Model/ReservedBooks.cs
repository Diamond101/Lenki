using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Model
{
    public class ReservedBooks
    {
        [Key]
        public int ReservedId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public bool Reserved { get; set; }
    }
}
