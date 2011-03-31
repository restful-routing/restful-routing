using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using RestfulRouting.Format;

namespace RestfulRouting.Spec.Format
{
    public class accept_list_sorting
    {
        Establish context = () =>
                                {
                                    _mimeTypes = new MimeTypeList();
                                    _mimeTypes.InitializeDefaults();
                                    _acceptList = new AcceptList(_mimeTypes, "text/html;q=0.3", "application/json; q=0.5", "multipart/form-data", "text/csv");
                               };

        Because of = () => _validTypes = _acceptList.Parse();

        It orders_correctly = () =>
                                  {
                                      _acceptList.First().Type.ShouldEqual("multipart/form-data");
                                      _acceptList[1].Type.ShouldEqual("text/csv");
                                      _acceptList[2].Type.ShouldEqual("application/json");
                                      _acceptList[3].Type.ShouldEqual("text/html");
                                                                    
                                  };

        static AcceptList _acceptList;
        static MimeTypeList _mimeTypes;
        static List<MimeType> _validTypes;
    }
}