using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Services.Contracts;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class SoftDeleteService<TEntity> : ISoftDeleteService<TEntity> where TEntity : IDeletableEntity
    {

        public void SoftDelete(TEntity entity)
        {
            entity.DeletedOn = DateTime.UtcNow;
            entity.IsDeleted = true;
        }

        public void Undelete(TEntity entity)
        {
            entity.DeletedOn = null;
            entity.IsDeleted = false;
        }
    }
}
