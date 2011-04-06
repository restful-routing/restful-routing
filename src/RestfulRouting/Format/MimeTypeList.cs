using System.Collections.Generic;
using System.Linq;

namespace RestfulRouting.Format
{
    public class MimeTypeList : List<MimeType>
    {
        Dictionary<string, MimeType> _lookup = new Dictionary<string, MimeType>();
        static MimeType _all = new MimeType("*/*", "all");

        public MimeType Register(string type, string format, params string[] synonyms)
        {
            var mimeType = new MimeType(type, format, synonyms);
            Add(mimeType);
            _lookup[type] = mimeType;
            foreach (var synonym in synonyms)
            {
                _lookup[synonym] = mimeType;
            }
            return mimeType;
        }

        public void Unregister(string type)
        {
            var mimeType = _lookup[type];
            if (mimeType != null)
            {
                _lookup.Remove(type);
                foreach (var synonym in mimeType.Synonyms)
                {
                    _lookup.Remove(synonym);
                }
                Remove(mimeType);
            }
        }

        public MimeType Lookup(string type)
        {
            if (type == "*/*")
                return _all;
            return _lookup[type];
        }

        public MimeType LookupByFormat(string format)
        {
            if (format == "all")
                return _all;
            return this.Where(x => x.Format == format).FirstOrDefault();
        }

        public List<MimeType> Parse(string accept)
        {
            return Parse(accept.Split(',').Select(x => x.Trim()).ToArray());
        }

        public List<MimeType> Parse(params string[] acceptTypes)
        {
            var acceptList = new AcceptList(this, acceptTypes);
            return acceptList.Parse();
        }

        public void InitializeDefaults()
        {
            Register("text/html", "html", "application/xhtml+xml");
            Register("text/plain", "text", "txt");
            Register("text/javascript", "js", "application/javascript", "application/x-javascript");
            Register("text/css", "css");
            Register("text/calendar", "ics");
            Register("text/csv", "csv");
            Register("application/xml", "xml", "text/xml", "application/x-xml");
            Register("application/rss+xml", "rss");
            Register("application/atom+xml", "atom");
            Register("application/x-yaml", "yaml", "text/yaml");

            Register("multipart/form-data", "multipart_form");
            Register("application/x-www-form-urlencoded", "url_encoded_form");

            Register("application/json", "json", "text/x-json", "application/jsonrequest");
        }

        public bool HasType(string type)
        {
            return _lookup.ContainsKey(type) || type == "*/*";
        }

        public IEnumerable<MimeType> Matching(string acceptType)
        {
            foreach (var type in this)
            {
                if (type.Matches(acceptType))
                    yield return type;
            }
        }
    }
}