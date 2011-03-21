using System.Web.Mvc;

namespace RestfulRouting.Spec.TestObjects
{
    public class CommentsController : Controller
    {
        public ActionResult Index(int? postId)
        {
            return Content("");
        }

        public ActionResult New(int? postId)
        {
            return Content("");
        }

        public ActionResult Create(int? postId)
        {
            return Content("");
        }

        public ActionResult Edit(int? postId, int id)
        {
            return Content("");
        }

        public ActionResult Update(int? postId, int id)
        {
            return Content("");
        }

        public ActionResult Delete(int? postId, int id)
        {
            return Content("");
        }

        public ActionResult Destroy(int? postId, int id)
        {
            return Content("");
        }

        public ActionResult Show(int? postId, int id)
        {
            return Content("");
        }
    }
}
