using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TurshiaWebApp.Models.Enums;

namespace TurshiaWebApp.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; } = false;

        [NotMapped]
        public int Level { get; set; } = 4;
        //TODO: GEnerate Level

        public  List<TaskSector> AffectedSectors { get; set; } = new List<TaskSector>();

        public string Description { get; set; }

        public string Participants { get; set; }


    }
}
