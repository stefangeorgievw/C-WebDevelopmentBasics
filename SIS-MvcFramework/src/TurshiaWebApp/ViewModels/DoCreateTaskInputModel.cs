using System;
using System.Collections.Generic;
using System.Text;
using TurshiaWebApp.Models;

namespace TurshiaWebApp.ViewModels
{
    public class DoCreateTaskInputModel
    {
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; } = false;

       

        public IEnumerable<TaskSector> AffectedSectors { get; set; } 

        public string Description { get; set; }

        public string Participants { get; set; }
    }
}
