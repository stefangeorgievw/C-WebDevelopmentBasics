using System;
using System.Collections.Generic;
using System.Text;
using TurshiaWebApp.Models.Enums;

namespace TurshiaWebApp.Models
{
    public class TaskSector
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }

        public Sectors Sector { get; set; }
    }
}
