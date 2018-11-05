using PandaWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status { get; set; }

        public DateTime? EstimateDeliveryDate { get; set; }

        public User Recipient { get; set; }
    }
}
