using Oren.Model.Model.Entities;
using Oren.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Service.Services
{
   public class LikeService:BaseService<Like>
    {
        public List<Like> get(int id )
        {
            List<Like> likes = GetByExp(x => x.ArticleID == id);
            return likes;
        }
       
    }
}
