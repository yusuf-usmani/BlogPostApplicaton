using BLL;
using BOL;
using System;
using System.Web.Mvc;

namespace BlogPostApp.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private UserBS userBs;
        public RegisterController()
        {
            userBs = new UserBS();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_user user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Role = "E";
                    userBs.Insert(user);
                    TempData["Msg"] = "Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Create Failed : " + e1.Message;
                return RedirectToAction("Index");
            }
        }
    }
}