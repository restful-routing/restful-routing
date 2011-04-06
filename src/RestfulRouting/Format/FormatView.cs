using System.Web.Mvc;

namespace RestfulRouting.Format
{
    public class FormatView : ViewResult
    {
        string _contentType;
        object _model;

        public FormatView(object model, string contentType)
        {
            _model = model;
            _contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData.Model = _model;
            base.ExecuteResult(context);
            context.HttpContext.Response.ContentType = _contentType;
        }
    }
}