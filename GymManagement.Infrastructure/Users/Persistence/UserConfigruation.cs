using GymManagement.Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Users.Persistence
{
    internal class UserConfigruation : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstNamme);

            builder.Property(x => x.LastName);

            builder.Property(x => x.Email);

            builder.Property(x => x.ParticipantId);

            builder.Property(x => x.AdminId);

            builder.Property(x => x.TrainerId);

            builder.Property(x => x._passwordHash)
                .HasColumnName("PasswordHash");

            
        }
    }
}
