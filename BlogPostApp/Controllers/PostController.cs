using BlogPostApp.ViewModel;
using BOL;
using System;
using System.IO;
using System.Web.Mvc;
using BlogPostApp.Constants;
using BLL;
using System.Linq;

namespace BlogPostApp.Controllers
{
    public class PostController : Controller
    {
        private UserBS userBS;
        private BlogBS blogBS;
        public PostController()
        {
            userBS = new UserBS();
            blogBS = new BlogBS();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogViewModel blogViewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(blogViewModel.Image.FileName);
            string extension = Path.GetExtension(blogViewModel.Image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            blogViewModel.Path = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            blogViewModel.Image.SaveAs(fileName);
            Blog blog = new Blog();
            blog.Content = blogViewModel.Content;
            blog.Title = blogViewModel.Title;
            blog.ImagePath = blogViewModel.Path;
            blog.StatusId = (int)BlogStatus.Draft;
            blog.userId = userBS.GetALL().Where(x => x.Email == User.Identity.Name).FirstOrDefault().userId;
            using (BlogPostEntities blogPostEntities = new BlogPostEntities())
            {
                blogPostEntities.Blogs.Add(blog);
                blogPostEntities.SaveChanges();
            }
            return RedirectToAction("Index","ListPost");
        }
    }
}