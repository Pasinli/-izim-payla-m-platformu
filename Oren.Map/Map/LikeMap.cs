using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class LikeMap:CoreMap<Like>
    {

        public LikeMap()
        {
            ToTable("Likes");

            Property(x => x.AppUserId).IsOptional();
            Property(x => x.ArticleID).IsOptional();
            Property(x => x.LastSeen).IsOptional().HasColumnType("datetime2");
              

        }
    }
}
