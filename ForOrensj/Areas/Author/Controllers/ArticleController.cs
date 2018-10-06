using ForOrensj.Areas.Author.Models;
using ForOrensj.Attributes;
using ForOrensj.Models;
using ForOrensj.Models.DTO;
using Oren.Model.Model.Entities;
using Oren.Service.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.OData;
using System.Web.Mvc;

namespace ForOrensj.Areas.Author.Controllers
{
    public class ArticleController : Controller
    {
        CommentService cmdb;
        ArticleService db;
        UserService userDb;
        LikeService likedb;
        MessageService msgdb;
       

        public ArticleController()
        {
            likedb = new LikeService();
            db = new ArticleService();
            userDb = new UserService();
            cmdb = new CommentService();
            msgdb = new MessageService();
        }
        [Role("Author", "Admin")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddArticle(Article model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string displayName = Path.GetFileName(file.FileName);
                var uploadDirection = Path.Combine(Server.MapPath("~/Content/img"), displayName);
                file.SaveAs(uploadDirection);
                model.ImagePath = displayName;

                if (ModelState.IsValid)
                {
                    TempData["durum"] = "Kayıt başarılı!";

                    model.UserID = userDb.GetByName(HttpContext.User.Identity.Name).ID;
                    db.Add(model);
                    return View();
                }
            }
            else
            {
                TempData["durum"] = "Kayıt Başarısız oldu!";
            }


            return View();
        }
        [Role("Author", "Admin")]
        [HttpGet]
        public ActionResult AddArticle()
        {
            TempData["durum"] = "";
            ArticleVM vm = new ArticleVM();

            AppUser user = new AppUser();

            user = userDb.GetByName(HttpContext.User.Identity.Name);
            vm.Nick = user.Nick;
            vm.UserId = user.ID;
            return View(vm);
        }

        [EnableQuery]
        public IQueryable ListArticles(int? id)
        {

            IEnumerable<ArticleVM> ArticleList = db.GetAll().Select(x => new ArticleVM
            {
                Nick = db.GetById(x.ID).User.Nick,
                Desc = x.Desc,
                ImagePath = x.ImagePath,
                UserId = x.UserID,
                Comments = x.ArtComments,
                Likes = x.Likes.Count,
                ArticleId = x.ID

            });




            return ArticleList.AsQueryable();
        }


        [Role("Admin", "Author")]
        public ActionResult DeleteArticle(int page = 1)
        {
            int id = userDb.GetByName(HttpContext.User.Identity.Name).ID;
            List<Like> UserLikes = likedb.GetByExp(x => x.AppUserId == id && x.LastSeen == null);
            foreach (var item in UserLikes)
            {
                item.LastSeen = DateTime.UtcNow;   
            }

            return View(db.GetByExp(x => x.User.Nick == HttpContext.User.Identity.Name).ToPagedList(page, 6));
        }

        [Role("Admin", "Author")]
        public ActionResult Delete(int? id)
        {
            db.DeleteById((int)id);
            return RedirectToAction("DeleteArticle", "Article", "Author");
        }

        [Role("Admin", "Author", "Member")]
        public ActionResult ShowArticle(int? id)
        {
            

            if (id != null)
            {
                ArticleCommentVM art = new ArticleCommentVM();
                
                art.Article = db.GetById((int)id);
                art.Likes = likedb.get((int)id);
                art.Comments = cmdb.GetByArticleId((int)id, 10);
                return View(art);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
        [Role("Admin", "Author", "Member")]
        public ActionResult AddComment(Comment data)
        {

            data.UserID = userDb.GetByName(HttpContext.User.Identity.Name).ID;
            cmdb.Add(data);
            return RedirectToAction("ShowArticle","Article",new {area="Author",id=data.ArticleID });
        }


        public ActionResult GetArticlesWhoMostLiked(int page=1)
        {
            return View(db.GetAll().OrderByDescending(x=>x.Likes.Count).ToPagedList(page,6));
        }

    }


}
