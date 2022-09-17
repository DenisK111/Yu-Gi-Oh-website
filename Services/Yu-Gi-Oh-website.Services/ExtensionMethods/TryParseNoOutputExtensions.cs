using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Enums;

namespace Yu_Gi_Oh_website.Services.ExtensionMethods
{
    internal static class TryParseNoOutputExtensions
    {
        public static bool IntTryParseNoOutput(this string input)
        {
            return int.TryParse(input, out int x);
        }

        public static bool EnumTryParseNoOutput(this string input)
        {
            return Enum.TryParse<CardTypeEnum>(input, out CardTypeEnum x);
        }
    }
}
