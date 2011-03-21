using System.Web.Mvc;

namespace RestfulRouting.Spec.TestObjects
{
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("");
        }

        public ActionResult New()
        {
            return Content("");
        }

        public ActionResult Create()
        {
            return Content("");
        }

        public ActionResult Edit( int id)
        {
            return Content("");
        }

        public ActionResult Update( int id)
        {
            return Content("");
        }

        public ActionResult Delete( int id)
        {
            return Content("");
        }

        public ActionResult Destroy( int id)
        {
            return Content("");
        }

        public ActionResult Show( int id)
        {
            return Content("");
        }

        public ActionResult Post(int year, string slug)
        {
            return null;
        }

        public ActionResult Latest()
        {
            return null;
        }

        public ActionResult Hello(int id)
        {
            return null;
        }
    }
}
