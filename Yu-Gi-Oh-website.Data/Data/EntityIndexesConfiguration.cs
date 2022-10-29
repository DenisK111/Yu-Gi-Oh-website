using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;

namespace Yu_Gi_Oh_website.Data.Data
{


    public static class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder builder)
        {
            // IDeletableEntity.IsDeleted index
            var deletableEntityTypes = builder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                builder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
            }

            builder.Entity<Card>()
                .HasIndex(b => b.Name);
        }
    }
}
