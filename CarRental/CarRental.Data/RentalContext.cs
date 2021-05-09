using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using CarRental.Data.Identity;
using CarRental.Data.Entities;

namespace CarRental.Data
{
    public class RentalContext:IdentityDbContext<User>
    {
        public RentalContext(DbContextOptions<RentalContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<RentForm> RentForms { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
