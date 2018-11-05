namespace PandaWebApp.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using PandaWebApp.Models.Enums;
    using PandaWebApp.ViewModels.Home;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using System.Linq;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (!User.IsLoggedIn)
            {
                return this.View();
            }

            var pendingPackages = this.Db.Packages
                .Include(x => x.Recipient)
                .Where(x => x.Recipient.Username == User.Username)
                .Where(x=> x.Status == Status.Pending)
                .Select(x => new PackageViewModel
                {
                   Id = x.Id,
                   Description = x.Description,
                }
                ).ToList();

            var shippedPackages = this.Db.Packages
               .Include(x => x.Recipient)
               .Where(x => x.Recipient.Username == User.Username)
               .Where(x => x.Status == Status.Shipped)
               .Select(x => new PackageViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
               }
               ).ToList();

            var deliveredPackages = this.Db.Packages
               .Include(x => x.Recipient)
               .Where(x => x.Recipient.Username == User.Username)
               .Where(x => x.Status == Status.Delivered)
               .Select(x => new PackageViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
               }
               ).ToList();


            var model = new LoggedInViewModel
            {
                PendingPackages = pendingPackages,
                ShippedPackages = shippedPackages,
                DeliveredPackages = deliveredPackages,
            };

            

            return this.View("Home/LoggedIn", model);
        }
    }
}
