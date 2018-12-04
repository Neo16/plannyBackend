using Microsoft.EntityFrameworkCore;
using PlannyBackend.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlannyBackend.DAL.Configure
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder
                 .HasOne(p => p.User)
                 .WithMany(u => u.Participations)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            builder
              .HasOne(p => p.Planny)
              .WithMany(planny => planny.Participations)
              .HasForeignKey(p => p.PlannyId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
