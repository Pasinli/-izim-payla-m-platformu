using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class ArtCommentMap:CoreMap<ArtComment>
    {
        public ArtCommentMap()
        {
            ToTable("ArtComments");

            HasRequired(x => x.Article).WithMany(x => x.ArtComments).HasForeignKey(x => x.ArticleID).WillCascadeOnDelete(false);


        }


    }
}
