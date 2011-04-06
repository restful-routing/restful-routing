using System.Linq;
using System.Text.RegularExpressions;

namespace RestfulRouting.Format
{
    public class MimeType
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string[] Synonyms { get; set; }

        public MimeType(string type, string format, params string[] synonyms)
        {
            Type = type;
            Format = format;
            Synonyms = synonyms;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Type, Format);
        }

        public bool Matches(string acceptType)
        {
            var regex = new Regex(acceptType);
            return regex.IsMatch(Type) || Synonyms.Any(regex.IsMatch);
        }
    }
}