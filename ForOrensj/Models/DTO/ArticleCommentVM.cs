using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForOrensj.Models.DTO
{
    public class ArticleCommentVM
    {

        public IEnumerable<Like> Likes { get; set; }
        public Article Article { get; set; }

        public IEnumerable<Comment>  Comments{ get; set; }
    }
}