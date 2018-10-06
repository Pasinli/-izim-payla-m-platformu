using Oren.Core.Core.Entity;
using Oren.Map.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.DAL.DAL.Context
{
   public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString= @"Data Source = HAKANH\HAKAN;Initial Catalog=Orenjs;Integrated Security=SSPI;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ArtCommentMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new LikeMap());
            modelBuilder.Configurations.Add(new MessageMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ArtComment> ArtComments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }


        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            DateTime date = DateTime.UtcNow;
            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                entity.AddedDate = date;
            }


            return base.SaveChanges();
        }

    }
}
