using SIS.MvcFramework;
using TurshiaWebApp.Data;

namespace TurshiaWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new TurshiaContext();
        }

        public TurshiaContext Db { get; }
    }
}
