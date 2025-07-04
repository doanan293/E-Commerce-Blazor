using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Data {
    public class AppDbContext : IdentityDbContext<AppUser> {
        public AppDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Achieve> CheckoutAchieves { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<PaymentMethod>()
                .HasData(
                    new PaymentMethod {
                        Id = Guid.NewGuid(),
                        Name = "Credit Card",
                    });
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole {
                        Id = Guid.NewGuid().ToString(),
                        Name = "User",
                        NormalizedName = "USER"
                    });
        }
    }
}