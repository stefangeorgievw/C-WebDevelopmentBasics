using Microsoft.EntityFrameworkCore;
using PandaWebApp.ViewModels.Receipts;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PandaWebApp.Controllers
{
    class ReceiptsController:BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var receipt = this.Db.Receipts.Include(x=> x.Package).Include(x=> x.Recipient)
                .Where(x => x.Recipient.Username == User.Username)
                .Where(x => x.Id == id)
                .Select(x => new ReceiptDetailsViewModel
                {
                    Id = x.Id,
                    IssuedOn = x.IssueDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    DeliveryAddress = x.Package.ShippingAddress,
                    PackageWeight = x.Package.Weight,
                    PackageDescription = x.Package.Description,
                    Recipient = x.Recipient.Username,
                    Fee = x.Fee

                }).FirstOrDefault();

            if (receipt == null)
            {
                return BadRequestError("Invalid receipt id");
            }

            return this.View(receipt);
        }

        [Authorize]
        public IHttpResponse Index()
        {
            var receipts = this.Db.Receipts.Include(x=> x.Recipient)
                .Where(x=> x.Recipient.Username == User.Username)
                .Select(x => new AllViewModel
            {
                    Fee = x.Fee,
                    IssuedOn = x.IssueDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                    Recipient = x.Recipient.Username,
                    Id = x.Id
            }).ToArray();

            return this.View(receipts);
        }
    }
}
