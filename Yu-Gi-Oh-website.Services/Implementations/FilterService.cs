using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Contracts;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class FilterService : IFilterService<Expression<Func<Card>>>
    {
        
        public IQueryable<Expression<Func<Card>>> Search(IQueryable<Expression<Func<Card>>> query, string name)
        {
            return query.Where(x => x.Name == name);
        }
    }
}
