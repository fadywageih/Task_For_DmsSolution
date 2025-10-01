using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task_For_Dms.DAL.Models;

namespace Task_For_Dms.DAL.Presistance
{
    public class ApplicationDBContext : DbContext

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSet
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceProperty> DevicePropertys { get; set; }

        #endregion
    }
}
