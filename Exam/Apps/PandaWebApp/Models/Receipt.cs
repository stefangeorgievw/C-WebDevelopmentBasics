using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public decimal Fee { get; set; } 

        public DateTime IssueDate { get; set; } = DateTime.UtcNow.Date;

        public User Recipient { get; set; }

        public Package Package { get; set; }
    }
}
