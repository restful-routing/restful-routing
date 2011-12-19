using System.Web.Mvc;

namespace RestfulRouting.Format.ResultExposal
{
    public class ActionResultExposer
    {
        readonly FormatCollection formatCollection;

        public ActionResultExposer(FormatCollection formatCollection)
        {
            this.formatCollection = formatCollection;
        }

        public ActionResult Html()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Json()
        {
            throw new System.NotImplementedException();
        }
    }
}