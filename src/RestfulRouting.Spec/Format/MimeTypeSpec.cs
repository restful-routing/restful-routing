using Machine.Specifications;
using RestfulRouting.Format;

namespace RestfulRouting.Spec.Format
{
    public class mime_type_matches
    {
        Establish context = () =>
        {
            _matcher = "text/*";
        };

        private static string _matcher;

        It matches_html = () => new MimeType("text/html", "html").Matches(_matcher).ShouldBeTrue();

        It doesnt_match_xml = () => new MimeType("application/xml", "xml").Matches(_matcher).ShouldBeFalse();
    }
}