using System.Web.Mvc;
using RestfulRouting.Exceptions;

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
            if (!this.formatCollection.ContainsKey("html"))
            {
                string message = string.Format("Format you are trying to get is not registered. Requested format is {0}. Registered formats are {1}",
                    "html", string.Join(", ", this.formatCollection.Keys));
                throw new NotRegisteredFormatException(message);
            }

            return this.formatCollection["html"].Invoke();
        }

        public ActionResult Json()
        {
            throw new System.NotImplementedException();
        }
    }
}