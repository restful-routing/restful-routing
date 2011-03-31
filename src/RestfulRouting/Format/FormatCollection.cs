using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestfulRouting.Format
{
    public class FormatCollection : Dictionary<string, Func<ActionResult>>
    {
        public ActionResult Default { get; set; }

        public Func<ActionResult> Html { set { this["html"] = value; } }

        public Func<ActionResult> Xml { set { this["xml"] = value; } }

        public Func<ActionResult> Json { set { this["json"] = value; } }

        public Func<ActionResult> Js { set { this["js"] = value; } }

        public Func<ActionResult> Csv { set { this["csv"] = value; } }
    }
}