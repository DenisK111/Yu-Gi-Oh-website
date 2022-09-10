﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface IFilterService<T>
    {
        IQueryable<T> Search(IQueryable<T> query,string name);
    }
}
