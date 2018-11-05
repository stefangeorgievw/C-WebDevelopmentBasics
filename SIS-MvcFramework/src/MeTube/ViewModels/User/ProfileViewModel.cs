using System;
using System.Collections.Generic;
using System.Text;

namespace MeTube.ViewModels.User
{
    public class ProfileViewModel
    {
      public  string Username { get; set; }
        
        public string Email { get; set; }

        public List<MeTube.Models.Tube> Tubes { get; set; }
    }
}
