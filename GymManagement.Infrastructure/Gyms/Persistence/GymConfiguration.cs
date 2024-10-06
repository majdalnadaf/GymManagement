using GymManagement.Domains.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    internal class GymConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id)
                .ValueGeneratedNever();

            builder.Property<List<Guid>>("_roomIds")
                .HasColumnName("RoomIds")
                .HasListOfIdsConverter();


            builder.Property<List<Guid>>("_trainerIds")
                .HasColumnName("TrainerIds")
                .HasListOfIdsConverter();

            builder.Property(x => x.Name);
            builder.Property(x => x.SubscriptionId);


        }
    }
}
