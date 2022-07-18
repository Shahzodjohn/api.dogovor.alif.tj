using Entity.ContractChoice;
using Entity.Entities;
using Entity.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionProvider.Context
{
    public class AppDbСontext : DbContext
    {
        public AppDbСontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<StructuralSubdivision> StructuralSubdivisions { get; set; }
        public DbSet<TrustieFoundation> TrustieFoundations { get; set; }
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<PassportType> PassportTypes { get; set; }
        public DbSet<AgreementConcluder> AgreementConcluders { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<PartialPaymentOrder> PartialPaymentOrderNames { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<RendedServicesVariations> RendedServicesVariations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Role>().HasData(new Role { Id = 1, RoleName = "User"},
                                           new Role { Id = 2, RoleName = "Editor"},
                                           new Role { Id = 3, RoleName = "Admin"});
        }
    }
}
