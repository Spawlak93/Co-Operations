﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Co_Operations.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        [Required]
        public DateTime DOB { get; set; }

        public decimal FundsEarned
        {
            get
            {
                decimal total = 0;
                foreach (var product in Products)
                {
                    foreach (var t in product.Transactions)
                    {
                        total += t.NumberSold * (decimal)t.Transaction.Location.MakerCommisionPercent * product.Price;
                    }
                }
                foreach (var transaction in Sales)
                {
                    total += transaction.TotalSaleAmount * (decimal)transaction.Location.SalesCommisionPercent;
                }
                return total;
            }
        }

        public decimal FundsPayedOut
        {
            get
            {
                decimal total = 0;
                foreach (var collect in Collected)
                    total += collect.AmountCollected;
                return total;
            }
        }

        public decimal FundsOwed => FundsEarned - FundsPayedOut;

        public virtual ICollection<Transaction> Sales { get; set; } = new List<Transaction>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<CollectedCommission> Collected { get; set; } = new List<CollectedCommission>();
        public virtual ICollection<LocationUser> Locations { get; set; } = new List<LocationUser>();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<CollectedCommission> CollectedCommissions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<LocationUser> LocationUsers { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }
        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.UserId);
            }
        }
    }
}