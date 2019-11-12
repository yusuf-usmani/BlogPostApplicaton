using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BlogPostApp.ViewModel
{
    public class EditBlogViewModel
    {
        public string Title { get; set; }

        public string Path { get; set; }

        [DisplayName("Upload Image")]
        public HttpPostedFileBase Image { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public string ImageName { get; set; }
    }
}