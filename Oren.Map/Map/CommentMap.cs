using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class CommentMap:CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("Comments");
            Property(x => x.Text).IsOptional();
            Property(x => x.ArticleID).IsOptional();
            Property(x => x.UserID).IsOptional();
            HasRequired(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);
            HasMany(x => x.ArtComments).WithRequired(x => x.Comment).HasForeignKey(x => x.CommentID).WillCascadeOnDelete(false);
        }

    }
}
