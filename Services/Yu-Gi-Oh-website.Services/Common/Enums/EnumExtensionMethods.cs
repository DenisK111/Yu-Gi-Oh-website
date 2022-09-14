﻿using System;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Yu_Gi_Oh_website.Services.Common.Enums
{
    public static class EnumExtensionMethods
    {

        public static string GetDisplayName(this Enum enumValue)
        {

            var value = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName();

            if(value is null)
            {
                return enumValue.ToString();
            }

            return value;
        }
    }
}
