using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class ArticleMap:CoreMap<Article>
    {

        public ArticleMap()
        {
            ToTable("Articles");


            Property(x => x.Desc).IsOptional();
            Property(x => x.CommentID).IsOptional();
            Property(x => x.UserID).IsOptional();
            HasMany(x => x.Likes).WithRequired(x => x.Article).WillCascadeOnDelete(false);

        }

        

    }
}
