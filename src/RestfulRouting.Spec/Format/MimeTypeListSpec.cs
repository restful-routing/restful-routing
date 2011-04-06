using System.Collections.Generic;
using Machine.Specifications;
using RestfulRouting.Format;

namespace RestfulRouting.Spec.Format
{
    public class mime_type_list_register_base
    {
        private Establish context = () => _mimeTypeList = new MimeTypeList();
        protected static MimeType _mimeType;
        protected static MimeTypeList _mimeTypeList;
    }

    public class mime_type_list_register : mime_type_list_register_base
    {
        Because of = () => _mimeType = _mimeTypeList.Register("image/png", "png", "image/something");

        It can_find_by_type = () => _mimeTypeList.Lookup("image/png").ShouldEqual(_mimeType);

        It can_find_by_synonyms = () => _mimeTypeList.Lookup("image/something").ShouldEqual(_mimeType);
    }

    public class mime_type_list_unregister_by_type : mime_type_list_register_base
    {
        Establish context = () => _mimeTypeList.Register("image/png", "png", "image/something");

        Because of = () => _mimeTypeList.Unregister("image/png");
        
        It can_remove_by_type = () => _mimeTypeList.Count.ShouldEqual(0);
    }

    public class mime_type_list_unregister_by_synonym : mime_type_list_register_base
    {
        Establish context = () => _mimeTypeList.Register("image/png", "png", "image/something");

        Because of = () => _mimeTypeList.Unregister("image/something");

        It can_remove_by_synonym = () => _mimeTypeList.Count.ShouldEqual(0);
    }

    public class mime_type_parse_base
    {
        protected static MimeTypeList _mimeTypes;

        private Establish context = () =>
                                {
                                    _mimeTypes = new MimeTypeList();
                                    _mimeTypes.InitializeDefaults();
                                    _mimeTypes.Register("image/png", "png");
                                    _mimeTypes.Register("application/pdf", "pdf");
                                };

        protected static MimeType ByFormat(string format)
        {
            return _mimeTypes.LookupByFormat(format);
        }
    }

    public class mime_type_parse : mime_type_parse_base
    {
        It parses_text_with_trailing_star_at_the_beginning = () => _mimeTypes.Parse("text/*", "text/html", "application/json", "multipart/form-data")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("text"),
                                                             ByFormat("js"),
                                                             ByFormat("css"),
                                                             ByFormat("ics"),
                                                             ByFormat("csv"),
                                                             ByFormat("xml"),
                                                             ByFormat("yaml"),
                                                             ByFormat("json"),
                                                             ByFormat("multipart_form")
                                                         });

        It parses_text_with_trailing_star_at_the_end = () => _mimeTypes.Parse("text/html, application/json, multipart/form-data, text/*")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("json"),
                                                             ByFormat("multipart_form"),
                                                             ByFormat("text"),
                                                             ByFormat("js"),
                                                             ByFormat("css"),
                                                             ByFormat("ics"),
                                                             ByFormat("csv"),
                                                             ByFormat("xml"),
                                                             ByFormat("yaml")
                                                         });

        It parses_text_with_trailing_star = () => _mimeTypes.Parse("text/*")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("text"),
                                                             ByFormat("js"),
                                                             ByFormat("css"),
                                                             ByFormat("ics"),
                                                             ByFormat("csv"),
                                                             ByFormat("xml"),
                                                             ByFormat("yaml"),
                                                             ByFormat("json")
                                                         });

        

        It parse_without_q = () => _mimeTypes.Parse("text/xml,application/xhtml+xml,text/yaml,application/xml,text/html,image/png,text/plain,application/pdf,*/*")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("xml"),
                                                             ByFormat("yaml"),
                                                             ByFormat("png"),
                                                             ByFormat("text"),
                                                             ByFormat("pdf"),
                                                             ByFormat("all")
                                                         });

        It parse_broken_accept_lines = () => _mimeTypes.Parse("text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/*,,*/*;q=0.5")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("xml"),
                                                             ByFormat("png"), // changed
                                                             ByFormat("text"),
                                                             ByFormat("all")
                                                         });

        It parses_application_with_trailing_star = () => _mimeTypes.Parse("application/*")
                                        .ShouldEqual(new List<MimeType>
                                                         {
                                                             ByFormat("html"),
                                                             ByFormat("js"),
                                                             ByFormat("rss"),
                                                             ByFormat("atom"),
                                                             ByFormat("xml"), // moved
                                                             ByFormat("yaml"),
                                                             ByFormat("url_encoded_form"),
                                                             ByFormat("json"),
                                                             ByFormat("pdf")
                                                         });

        It parses_all = () => _mimeTypes.Parse("*/*").ShouldEqual(new List<MimeType> { ByFormat("all") });
    }
}