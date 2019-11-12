using BLL;
using BlogPostApp.ViewModel;
using BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BlogPostApp.Controllers
{
    public class ListPostController : Controller
    {
        private BlogBS blogBS;
        public ListPostController()
        {
            blogBS = new BlogBS();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var blogs = new List<Blog>();
            if(User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("A"))
                {
                     blogs = blogBS.GetALL();
                }
                if (User.IsInRole("E"))
                {
                    blogs = blogBS.GetALL().Where(x => x.tbl_user.Email == User.Identity.Name).ToList();
                }
            }
            else
            {
                blogs = blogBS.GetALL().Where(x => x.StatusId == 2).ToList();
            }
            
            return View(blogs);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blog = blogBS.GetByID(id);
            var editBlogViewModel = new EditBlogViewModel();
            editBlogViewModel.Content = blog.Content;
            editBlogViewModel.Path = blog.ImagePath;
            editBlogViewModel.Title = blog.Title;
            editBlogViewModel.BlogId = blog.BlogId;
            editBlogViewModel.ImageName = blog.ImagePath.Split('/')[blog.ImagePath.Split('/').Length - 1];
            return View(editBlogViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditBlogViewModel viewModel)
        {
            if(viewModel.Image != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(viewModel.Image.FileName);
                string extension = Path.GetExtension(viewModel.Image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                viewModel.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                viewModel.Image.SaveAs(fileName);
            }

            var existingBlog = blogBS.GetByID(viewModel.BlogId);
            Blog blog = new Blog();
            blog.BlogId = viewModel.BlogId;
            blog.Title = viewModel.Title;
            blog.Content = viewModel.Content;
            blog.ImagePath = viewModel.Path ?? existingBlog.ImagePath;
            blog.userId = existingBlog.userId;
            blog.Status = existingBlog.Status;
            blog.StatusId = existingBlog.StatusId;
            blogBS.Update(blog);
           return  RedirectToAction("Index");
        }

        public ActionResult Approve(int id)
        {
            var blog = blogBS.GetByID(id);
            blog.StatusId = 2;
            blogBS.Update(blog);
            TempData["Msg"] = "Blog with title:" + blog.Title + " Approved";
            return RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            var blog = blogBS.GetByID(id);
            blog.StatusId = 3;
            blogBS.Update(blog);
            TempData["Msg"] = "Blog with title:" + blog.Title + " Approved";
            return RedirectToAction("Index");
        }
    }
}