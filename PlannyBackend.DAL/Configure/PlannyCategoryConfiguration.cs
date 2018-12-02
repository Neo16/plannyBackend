using Microsoft.EntityFrameworkCore;
using PlannyBackend.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlannyBackend.DAL.Configure
{
    public class PlannyCategoryConfiguration : IEntityTypeConfiguration<PlannyCategory>
    {
        public void Configure(EntityTypeBuilder<PlannyCategory> builder)
        {
            builder
              .HasKey(a => new { a.PlannyId, a.CategoryId });
        }
    }
}
