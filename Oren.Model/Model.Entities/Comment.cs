using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class Comment :CoreEntity
    {

        public Comment()
        {
            ArtComments = new HashSet<ArtComment>();
        }

        public int UserID { get; set; }
        public int ArticleID { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<ArtComment> ArtComments { get; set; }

        public string Text { get; set; }


        

    }
}
