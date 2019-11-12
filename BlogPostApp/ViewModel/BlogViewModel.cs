using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogPostApp.ViewModel
{
    public class BlogViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Path { get; set; }

        [DisplayName("Upload Image")]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        public string Content { get; set; }
    }
}