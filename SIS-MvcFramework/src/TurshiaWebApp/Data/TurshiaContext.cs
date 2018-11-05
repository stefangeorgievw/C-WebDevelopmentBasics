using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TurshiaWebApp.Models;

namespace TurshiaWebApp.Data
{
    public class TurshiaContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Report> Report { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskSector> TasksSectors { get; set; }



        public TurshiaContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=Turshia;Integrated Security=True;");
        }
    }
}
