using System.Text;
using System.Text.RegularExpressions;

namespace Yu_Gi_Oh_website.Common
{

    public static class SlugGenerator
    {
        public static string ToUrlSlug(this string value)
        {
            //First to lower case
            string smallValue = value.Substring(0, Math.Min(value.Length,51)).ToLowerInvariant();
            value = smallValue;
            if (value.Length>= 50)
            {
                value = smallValue.Substring(0, smallValue.LastIndexOf(' '));
            }                   

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
    }
}