using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Common.ValidationAttributes
{
    public class MaxLengthAfterRemovedHtmlAttribute : ValidationAttribute
    {
        private readonly uint minLength;
        private readonly uint maxLength;

        public MaxLengthAfterRemovedHtmlAttribute(uint minLength, uint maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object? value)
        {
            var text = value as string;

            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var regex = @"<[^>]+>";

            text = Regex.Replace(text!, regex, "");

            if (text.Length > maxLength || text.Length < minLength)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            var minLengthMessage = minLength == 0 ? "must not be empty" : $"must be at least {minLength} characters long";

            return $"{name} {minLengthMessage} and must contain maximum {maxLength} characters.";
        }
    }
}
