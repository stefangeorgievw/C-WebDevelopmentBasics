using MeTube.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeTube.Controllers
{
    public class UserController : BaseController
    {
        [Authorize]
        public IHttpResponse Profile()
        {
            var user = this.Db.Users.Include(x => x.Tubes).FirstOrDefault(x => x.Username == User.Username);

            if (user == null)
            {
                return BadRequestError("Error");
            }

            var model = new ProfileViewModel
            {
                Username = user.Username,
                Email = user.Email.Replace("@", "&commat;"),
                Tubes = user.Tubes.ToList(),
            };
            
            return this.View(model);
        }
    }
}
