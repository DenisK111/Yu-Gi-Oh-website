﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface IFilterService
    {
        IQueryable<Card> Search(IQueryable<Card> query,string name);
    }
}
