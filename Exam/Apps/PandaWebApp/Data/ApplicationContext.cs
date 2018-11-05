using Microsoft.EntityFrameworkCore;
using PandaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=Panda;Integrated Security=True;");
        }
    }
}
