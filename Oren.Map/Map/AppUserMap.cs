using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class AppUserMap: CoreMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("AppUser");

            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity).HasColumnOrder(1);
            Property(x => x.ArticleID).IsOptional();
            Property(x => x.CommentID).IsOptional();
            Property(x => x.LikeID).IsOptional();
            Property(x => x.Role).IsOptional();
            Property(x => x.Nick).IsOptional();
            Property(x => x.AddedDate).HasColumnType("datetime2");
            Property(x => x.Phone).IsOptional();
            Property(x => x.Password).IsOptional();
            HasMany(x => x.Likes).WithRequired(x => x.User).HasForeignKey(x=>x.AppUserId).WillCascadeOnDelete(false);
            HasMany(x => x.Articles).WithRequired(x => x.User).HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);

            HasMany(x => x.SendedMessages).WithRequired(x => x.Sender).HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);



        }

    }
}
