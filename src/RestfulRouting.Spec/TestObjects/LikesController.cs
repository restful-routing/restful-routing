using System.Web.Mvc;

namespace RestfulRouting.Spec.TestObjects
{
    public class LikesController : Controller
    {
        public ActionResult Index(int? postId, int? commentId)
        {
            return Content("");
        }

        public ActionResult New(int? postId, int? commentId)
        {
            return Content("");
        }

        public ActionResult Create(int? postId, int? commentId)
        {
            return Content("");
        }

        public ActionResult Edit(int? postId, int? commentId, int id)
        {
            return Content("");
        }

        public ActionResult Update(int? postId, int? commentId, int id)
        {
            return Content("");
        }

        public ActionResult Delete(int? postId, int? commentId, int id)
        {
            return Content("");
        }

        public ActionResult Destroy(int? postId, int? commentId, int id)
        {
            return Content("");
        }

        public ActionResult Show(int? postId, int? commentId, int id)
        {
            return Content("");
        }
    }
}
