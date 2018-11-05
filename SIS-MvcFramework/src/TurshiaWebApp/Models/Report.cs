using System;
using System.Collections.Generic;
using System.Text;
using TurshiaWebApp.Models.Enums;

namespace TurshiaWebApp.Models
{
    public class Report
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime ReportedOn { get; set; } = DateTime.UtcNow.Date;

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }
    }
}
