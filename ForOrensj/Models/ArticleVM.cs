using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForOrensj.Models
{
    public class ArticleVM
    {

        public ArticleVM()
        {
            Image = new Image();
            User = new AppUser();
        }
        public AppUser User { get; set; }
        public Image Image;
        public string Desc { get; set; }
    }
}