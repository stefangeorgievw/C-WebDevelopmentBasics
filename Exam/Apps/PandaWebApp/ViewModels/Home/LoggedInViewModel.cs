using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Home
{
    public class LoggedInViewModel
    {
       public IEnumerable<PackageViewModel> PendingPackages { get; set; }
       
       public IEnumerable<PackageViewModel> ShippedPackages { get; set; }
       
       public IEnumerable<PackageViewModel> DeliveredPackages { get; set; }
    }
}
