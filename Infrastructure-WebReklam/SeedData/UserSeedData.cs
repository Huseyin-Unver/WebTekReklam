using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_WebReklam.SeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = "e3569982-6bcc-49ed-b599-6b67ae72d134",
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMİN.COM",
                PhoneNumber = "1234567890",
                PasswordHash = hasher.HashPassword(null, "Abcd!1234"),
                LockoutEnabled = true
            };
            builder.HasData
            (
                admin
            );
        }

       
    }
}
