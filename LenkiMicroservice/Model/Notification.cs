using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.Model
{
    public class Notifications
    {
        [Key]
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public DateTime DateCreated { get; set; }
    }


    public class Notification
    {
        
        public int CustomerId { get; set; }
        public int BookId { get; set; }
    }
}
