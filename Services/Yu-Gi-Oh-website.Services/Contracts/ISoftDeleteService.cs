using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface ISoftDeleteService<TEntity>
    {
        public void SoftDelete(TEntity entity);
        public void Undelete(TEntity entity);

    }
}
