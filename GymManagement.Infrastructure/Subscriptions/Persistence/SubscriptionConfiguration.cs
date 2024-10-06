using GymManagement.Domains.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Id)
                .ValueGeneratedNever();

            builder.Property("_adminId")
                .HasColumnName("AdminId");

            builder.Property(x => x.SubscriptionType)
                .HasConversion(
            subscriptionType => subscriptionType.Value,
            value => SubscriptionType.FromValue(value)
            );

            builder.Property<List<Guid>>("_gymIds")
                   .HasColumnName("GymIds")
                   .HasListOfIdsConverter();

        }
    }
}
