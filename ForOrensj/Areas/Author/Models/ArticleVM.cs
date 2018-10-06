using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForOrensj.Areas.Author.Models
{
    public class ArticleVM
    {

        public ArticleVM()
        {
            Comments = new HashSet<ArtComment>();
        }
        public int? UserId { get; set; }

        public string Nick { get; set; }

        public string Desc { get; set; }

        public string ImagePath { get; set; }

        public int ArticleId { get; set; }

        public int Likes { get; set; }

        public  IEnumerable<ArtComment> Comments { get; set; }

    }
}