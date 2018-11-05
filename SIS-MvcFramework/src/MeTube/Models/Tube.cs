using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeTube.Models
{
   public class Tube
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string  Description { get; set; }

        public string YoutubeId { get; set; }

        public int Views { get; set; } = 0;

        public User Uploader { get; set; }


    }
}
