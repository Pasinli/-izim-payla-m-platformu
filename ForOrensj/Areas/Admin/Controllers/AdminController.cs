using ForOrensj.Attributes;
using Oren.Model.Model.Entities;
using Oren.Service.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForOrensj.Areas.Admin.Controllers
{
    [Role("Admin")]
    public class AdminController : Controller
    {
        UserService db = new UserService();
        ArticleService artdb = new ArticleService();
        MessageService msgdb = new MessageService();
        CommentService cmddb = new CommentService();
        ArtCommentService artcmddb = new ArtCommentService();
        LikeService likedb = new LikeService();
        // GET: Admin/Admin
        public ActionResult GivePermission(int page = 1)
        {
            return View(db.GetAll().OrderByDescending(x=>x.AddedDate).Where(x => x.isArtist == true && x.Seen == true).ToPagedList(page, 3));
        }
        [HttpPost]
        public ActionResult GivePermission(int? id)
        {
            AppUser user = new AppUser();
            user = db.GetById((int)id);
            user.isArtist = true;
            user.Seen = false;
            user.Role = Oren.Core.Core.Entity.Enum.Role.Author;
            db.Save();
            return RedirectToAction("GivePermission", "Admin", new { @area = "Admin" });
        }

        public ActionResult DontGiveAS(int? id)
        {
            AppUser user = new AppUser();
            user = db.GetById((int)id);
            user.isArtist = false;
            user.isArtist = false;
            db.Save();
            return RedirectToAction("GivePermission", "Admin", new { @area = "Admin" });
        }

        public ActionResult RemovePerm(int? id)
        {
            AppUser user = new AppUser();
            user = db.GetById((int)id);
            user.Role = Oren.Core.Core.Entity.Enum.Role.Member;
            db.Save();

            return RedirectToAction("Users","Admin",new {area="Admin" });
        }

        public ActionResult DeleteUser(int id)
        {
           List<Message> Sendedmsg = msgdb.GetByExp(x => x.SenderId == id);

            List<Message> Receivedmsg = msgdb.GetByExp(x => x.ReceivederId == id);

            List<ArtComment> ArtComments = artcmddb.GetByExp(x => x.Comment.UserID == id);

            List<Comment> Comments = cmddb.GetByExp(x => x.UserID == id);

            List<Article> Articles = artdb.GetByExp(x => x.UserID == id);
            List<Like> Likes = likedb.GetByExp(x => x.AppUserId == id);

            likedb.DeleteAll(Likes);
            artcmddb.DeleteAll(ArtComments);
            msgdb.DeleteAll(Sendedmsg);
            msgdb.DeleteAll(Receivedmsg);
            cmddb.DeleteAll(Comments);
            artdb.DeleteAll(Articles);
           
            db.DeleteById(id);
            db.Save();

            return RedirectToAction("Users", "Admin", new { area = "Admin" });
        }


        public ActionResult Articles(int page = 1)
        {
            
            return View(artdb.GetAll().OrderByDescending(x => x.AddedDate).ToPagedList(page, 6));

        }

        public ActionResult DeleteArticles(int? id)
        {
            if (id != null)
            {
                Article art = artdb.GetById((int)id);
                artdb.Delete(art);
            }
            return RedirectToAction("Articles","Admin","Admin");
        }

        public ActionResult Users(string id,int page=1 )
        {
            List<AppUser> Users = new List<AppUser>();
            Users = db.GetAll();
            if(id!=null)
            {
                Users.Clear();
                Users = db.GetByExp(x => x.Nick==id);
            }


            return View(Users.ToPagedList(page,1));
        }




    }
}