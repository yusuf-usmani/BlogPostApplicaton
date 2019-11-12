using BLL;
using System.Web.Mvc;

namespace BlogPostApp.Controllers
{
    [Authorize(Roles = "A")]
    public class ListUserController : Controller
    {
        private UserBS userBS;
        public ListUserController()
        {
            userBS = new UserBS();
        }
        // GET: ListUser
        public ActionResult Index()
        {
            var users = userBS.GetALL();
            return View(users);
        }
    }
}