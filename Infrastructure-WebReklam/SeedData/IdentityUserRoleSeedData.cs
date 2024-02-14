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
    public class IdentityUserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
                 (
                     new IdentityUserRole<string>
                     {
                         UserId = "e3569982-6bcc-49ed-b599-6b67ae72d134",
                         RoleId = "2941c96f-580d-4fa1-a18f-80dc158b28cb"
                     }
                     );

        }
    }
}
