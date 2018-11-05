using Microsoft.EntityFrameworkCore;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels;
using PandaWebApp.ViewModels.Home;
using PandaWebApp.ViewModels.Packages;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PandaWebApp.Controllers
{
    public class PackagesController:BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {

            var package = this.Db.Packages.Include(x => x.Recipient).Where(x => x.Id == id)
                .Where(x => x.Recipient.Username == User.Username)
                .Select(x => new DetailsViewModel
                {Address = x.ShippingAddress,
                 Status = x.Status.ToString(),
                 EstimateDeliveryDate = x.EstimateDeliveryDate.HasValue ? x.EstimateDeliveryDate.Value.ToString("dd/M/yyyy",CultureInfo.InvariantCulture) : "N/A",
                 Weight = x.Weight,
                 Recipient = x.Recipient.Username,
                 Description = x.Description
                      }).FirstOrDefault();

            if (package == null)
            {

                return   BadRequestError("Invalid package id");
            }
            return this.View(package);
        }

        [Authorize("Admin")]
        public IHttpResponse Create()
        {
            var users = this.Db.Users.Select(x => x).ToArray();

            return this.View(users);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(CreatePackageViewModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Recipient);

            if (user == null)
            {
                return BadRequestError("Invalid recipient");
            }
            var package = new Package
            {
                Description = model.Description,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
                Recipient = user,
                Status = Status.Pending,
                EstimateDeliveryDate = null,

            };

           
            
            this.Db.Packages.Add(package);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize("Admin")]
        public IHttpResponse Pending()
        {
            var pendingPackages = this.Db.Packages
               .Include(x => x.Recipient)
               .Where(x => x.Status == Status.Pending)
               .Select(x => new PackagesViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Weight = x.Weight,
                   ShippingAddress = x.ShippingAddress,
                   Recipient = x.Recipient.Username,
               }
               ).ToArray();


            return this.View(pendingPackages);
        }

        [Authorize("Admin")]
        public IHttpResponse Ship(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return BadRequestError("Invalid package id");
            }

            var random = new Random();
          var days = random.Next(20, 40);

            package.Status = Status.Shipped;
            package.EstimateDeliveryDate = DateTime.UtcNow.Date.AddDays(days);

            this.Db.SaveChanges();

            return this.Redirect("/");

        }


        [Authorize("Admin")]
        public IHttpResponse Shipped()
        {
            var shippedPackages = this.Db.Packages
               .Include(x => x.Recipient)
               .Where(x => x.Status == Status.Shipped)
               .Select(x => new PackagesViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Weight = x.Weight,
                   ShippingAddress = x.ShippingAddress,
                   Recipient = x.Recipient.Username,
               }
               ).ToArray();


            return this.View(shippedPackages);
        }


        [Authorize("Admin")]
        public IHttpResponse Deliver(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return BadRequestError("Invalid package id");
            }

            

            package.Status = Status.Delivered;
            

            this.Db.SaveChanges();

            return this.Redirect("/");

        }

        [Authorize("Admin")]
        public IHttpResponse Delivered()
        {
            var deliveredPackages = this.Db.Packages
               .Include(x => x.Recipient)
               .Where(x => x.Status == Status.Delivered)
               .Select(x => new PackagesViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Weight = x.Weight,
                   ShippingAddress = x.ShippingAddress,
                   Recipient = x.Recipient.Username,
               }
               ).ToArray();


            return this.View(deliveredPackages);
        }

        [Authorize("Admin")]
        public IHttpResponse Aquire(int id)
        {
            var package = this.Db.Packages.Include(x=> x.Recipient).FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return BadRequestError("Invalid package id");
            }

            decimal multyplier = 2.67M;

            package.Status = Status.Acquired;
            var receipt = new Receipt
            {
                Package = package,
                Fee = (decimal)package.Weight * multyplier,
                Recipient = package.Recipient,
            };

            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();
            
            return this.Redirect("/");

        }
    }
}
