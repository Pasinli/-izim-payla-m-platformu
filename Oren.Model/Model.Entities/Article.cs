using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class Article:CoreEntity
    {

        
        public Article()
        {
            Likes = new HashSet<Like>();
            ArtComments = new HashSet<ArtComment>();
        }


        public int? UserID { get; set; }
        public string Desc { get; set; }
        public int? CommentID { get; set; }
        public int? LikeID { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<ArtComment> ArtComments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual AppUser User { get; set; }

    }
}
