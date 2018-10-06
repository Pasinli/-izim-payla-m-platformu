using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class ArtComment :CoreEntity
    {
        public int ArticleID { get; set; }

        public int CommentID { get; set; }

        public virtual Article Article { get; set; }

        public virtual Comment Comment { get; set; }

    }
}
