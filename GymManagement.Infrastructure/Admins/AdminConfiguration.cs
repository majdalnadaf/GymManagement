using GymManagement.Domains.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            // This method inital the instance admin into the database during the migration process

            builder.HasData(new Admin(
                userId: Guid.NewGuid(),
                id: Guid.Parse("9ca5b963-c1f5-4c06-905b-b6d691d2a8e6")
                ));
        }
    }
}
