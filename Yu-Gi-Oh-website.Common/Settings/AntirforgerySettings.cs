﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Common.Settings
{
    public static class AntiforgerySettings
    {
        public static string HeaderName => "X-CSRF-TOKEN";
    }
}
