using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurshiaWebApp.ViewModels;

namespace TurshiaWebApp.Controllers
{
    public class HomeController:BaseController
    {
        public IHttpResponse Index()
        {
            if (User.IsLoggedIn)
            {
                var tasks = this.Db.Tasks.Select(x => new TaskViewModel
                {
                    Title = x.Title,
                    Level = x.Level,
                }).ToList();

                var model = new IndexViewModel { Tasks = tasks };
               
                return this.View("Home/LoggedInUser", model);
            }
            return this.View();
        }
    }
}
