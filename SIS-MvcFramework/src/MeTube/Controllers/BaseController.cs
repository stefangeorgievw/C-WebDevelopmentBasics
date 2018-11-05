using MeTube.Data;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeTube.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new ApplicationContext();
        }

        public ApplicationContext Db { get; }
    }
}
