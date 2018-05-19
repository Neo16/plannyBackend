using Microsoft.EntityFrameworkCore;
using PlannyBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlannyBackend.Data.Configure
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            builder
                 .HasOne(p => p.User)
                 .WithMany(u => u.Participations)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.SetNull);

            builder
              .HasOne(p => p.PlannyProposal)
              .WithMany(planny => planny.Participations)
              .HasForeignKey(p => p.PlannyProposalId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
