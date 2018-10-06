using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class Like:CoreEntity
    {
        public int ArticleID { get; set; }
        public int AppUserId { get; set; }
        public Article Article { get; set; }
        public AppUser User { get; set; }
        public string LikerName { get; set; }

        public DateTime? LastSeen { get; set; }
    }
}
