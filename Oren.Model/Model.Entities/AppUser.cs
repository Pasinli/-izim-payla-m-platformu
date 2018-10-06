using Oren.Core.Core.Entity;
using Oren.Core.Core.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class AppUser :CoreEntity
    {

        public AppUser()
        {
            Articles = new HashSet<Article>();
            Likes = new HashSet<Like>();
            Comments = new HashSet<Comment>();
        }

        public string Nick { get; set; }

        public string NameSurname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string AboutMe { get; set; }

        public bool isArtist { get; set; }

        public bool Seen { get; set; }

        public string Phone { get; set; }

        public Role? Role { get; set; }

        public bool isAllPropsFilled { get; set; }
        public int? CommentID { get; set; }

        public int? LikeID { get; set; }

        public int? ArticleID { get; set; }

        public string  ProfileImage { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Message> SendedMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }



    }
}
