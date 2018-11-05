using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeTube.Controllers
{
   public  class HomeController:BaseController
    {
        public IHttpResponse Index()
        {
            if (!User.IsLoggedIn)
            {
                return this.View();
            }

            var user = this.Db.Users.Include(x=> x.Tubes).FirstOrDefault(x=> x.Username == User.Username);

            var tubes = user.Tubes.ToArray();
            

            return this.View("Home/LoggedInUser",tubes);
            
        }
    }
}
