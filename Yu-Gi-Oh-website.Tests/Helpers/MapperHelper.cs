using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Yu_Gi_Oh_website.Web.AutoMapper;

namespace Yu_Gi_Oh_website.Tests.Helpers
{
    internal static class MapperHelper
    {
        internal static IMapper Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ForumProfile());
                cfg.AddProfile(new CardProfile());
            }).CreateMapper();

    }
}
