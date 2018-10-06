using ForOrensj.Attributes;
using ForOrensj.Models;
using ForOrensj.Models.DTO;
using Oren.Core.Core.Entity.Enum;
using Oren.Model.Model.Entities;
using Oren.Service.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ForOrensj.Controllers
{

    public class AccountController : Controller
    {
        UserService db = new UserService();
        LikeService likeDb = new LikeService();
        UserService userDb = new UserService();
        MessageService msgDb = new MessageService();
        ArticleService artdb = new ArticleService();
        // GET: Account
        [HttpGet]
        public ActionResult SendMessage()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult ShowMessage(int? id, int page = 1)
        {
            Message msg = msgDb.GetById((int)id);
            MessageVM msgvm = new MessageVM();


            IEnumerable<MessageVM> MessageList = msgDb.GetByExp(x => x.ReceivederId == msg.ReceivederId || x.ReceivederId == msg.SenderId && x.SenderId == msg.SenderId).OrderByDescending(x => x.AddedDate).Select(x =>
                 {
                     int senderId = x.SenderId;
                     string senderName = userDb.GetById(senderId).Nick;
                     return new MessageVM
                     {
                         Id = x.ID,
                         SenderId = senderId,
                         ReceivedById = x.ReceivederId,
                         Text = x.Text,
                         Sender = senderName
                     };
                 }).ToPagedList(page, 5);



            return View(MessageList);
        }

        [HttpPost]
        public ActionResult SendMessage(Message data)
        {
            if (data.Text != null && data.ReceivedBy.Nick != string.Empty)
            {
                data.SenderId = userDb.GetByName(HttpContext.User.Identity.Name).ID;
                data.ReceivederId = userDb.GetByName(data.ReceivedBy.Nick).ID;
                data.ReceivedBy = null;
                msgDb.Add(data);
                ViewBag.Message = "Gönderim Başarılı!";
            }
            else
            {
                ViewBag.Message = "Lütfen Alanları Doldurunuz";
            }
            return View();
        }


        public JsonResult ReceiveMessages(Message data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Messages(int page = 1)
        {
            int id = userDb.GetByName(HttpContext.User.Identity.Name).ID;

            List<Message> msg = msgDb.GetByExp(x => x.ReceivederId == id);
            //Mesajları son görme tarihi.. bildirim için
            if (msg != null)
            {
                foreach (var item in msg)
                {
                    if (item.LastCheck == null)
                    {
                        item.LastCheck = DateTime.UtcNow;

                    }
                }
                msgDb.Save();
            }
            IEnumerable<MessageVM> MessageList = msgDb.GetByExp(x => x.ReceivederId == id).OrderByDescending(x => x.AddedDate).Select(x =>
                  {
                      int senderId = x.SenderId;
                      string senderName = userDb.GetById(senderId).Nick;
                      return new MessageVM
                      {
                          Id = x.ID,
                          SenderId = senderId,
                          ReceivedById = x.ReceivederId,
                          Text = x.Text,
                          Sender = senderName


                      };
                  }).ToPagedList(page, 5);




            return View(MessageList);
        }



        public JsonResult MessageInfo()
        {
            int id = userDb.GetByName(HttpContext.User.Identity.Name).ID;
            int msgCount = msgDb.GetByExp(x=>x.LastCheck ==null&&x.ReceivederId==id).Count;
            MessageCountDTO count = new MessageCountDTO();
            count.MessageCount = msgCount;
            return Json(count, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Login()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = db.GetByMail(HttpContext.User.Identity.Name);
                if (user.Role == Role.Member) return RedirectToAction("Index", "Home", new { area = "" });
                else if (user.Role == Role.Admin)
                    return RedirectToAction("GivePermission", "Admin", new { area = "Admin" });
                else if (user.Role == Role.Author) return RedirectToAction("Index", "Home", new { area = "Author" });

            }
            return View();

        }
        [Role("Member", "Author", "Admin")]
        public ActionResult Profil(UserUpdateVM user)
        {
            AppUser x = new AppUser();
            x = db.UserFromName(HttpContext.User.Identity.Name);
            if (x.isAllPropsFilled == false)
            {
                user.Nick = x.Nick;
                user.Password = x.Password;
                user.Email = x.Email;
                return View(user);
            }
            else
                return RedirectToAction("FullProfile", "Account");


        }
        [Role("Member", "Author", "Admin")]
        public ActionResult FullProfile(int? id)
        {
            AppUser u;
            if (id != null)
            {
                u = db.GetById((int)id);
                if (u.isAllPropsFilled == false)
                {
                    return RedirectToAction("Profil", "Account", new { area = "" });
                }
                return View(u);
            }

            u = db.UserFromName(HttpContext.User.Identity.Name);
            return View(u);

        }



        [Role("Member", "Author", "Admin")]
        [HttpPost]
        public ActionResult FullProfile(AppUser user)
        {
            user = db.GetByName(HttpContext.User.Identity.Name);
            user.isAllPropsFilled = false;
            db.Save();
            return RedirectToAction("Profil", "Account");

        }

        [Role("Member", "Author", "Admin")]
        [HttpPost]
        public ActionResult Profil(UserUpdateVM data, HttpPostedFileBase file)
        {
            string path;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string displayName = Path.GetFileName(file.FileName);
                    var uploadDirection = Path.Combine(Server.MapPath("~/Content/pp"), displayName);
                    file.SaveAs(uploadDirection);
                    path = displayName;
                }
                else
                    path = "defaultpp.png";


                string selectedRole = Request.Form["roleselect"].ToString();
                if (selectedRole == "Ressam")
                {
                    AppUser user = new AppUser()
                    {
                        Nick = data.Nick,
                        Password = data.Password,
                        Role = Role.Member,
                        isAllPropsFilled = true,
                        Email = data.Email,
                        ID = db.GetByName(HttpContext.User.Identity.Name).ID,
                        NameSurname = data.NameSurname,
                        Phone = data.Phone,
                        ProfileImage = path,
                        AboutMe = data.Hakkimda,
                        isArtist = true,
                        Seen = true

                    };
                    db.Update(user);
                }
                else if (selectedRole == "Kullanıcı")
                {
                    AppUser user = new AppUser()
                    {
                        Nick = data.Nick,
                        Password = data.Password,
                        Role = Role.Member,
                        isAllPropsFilled = true,
                        Email = data.Email,
                        ID = db.GetByName(HttpContext.User.Identity.Name).ID,
                        NameSurname = data.NameSurname,
                        Phone = data.Phone,
                        ProfileImage = path,
                        AboutMe = data.Hakkimda,
                        isArtist = false,
                        Seen = true

                    };
                    db.Update(user);
                }

                return RedirectToAction("FullProfile", "Account");
            }
            return View();


        }






        [HttpPost]
        public ActionResult Login(UserVM data)
        {
            if (db.UserControl(data.Email, data.Password))
            {
                AppUser currentUser = db.GetByMail(data.Email);
                string cookie = currentUser.Nick;
                FormsAuthentication.SetAuthCookie(cookie, true);


                if (currentUser.Role == Role.Member) return RedirectToAction("Index", "Home", new { area = "" });
                else if (currentUser.Role == Role.Admin)
                    return RedirectToAction("GivePermission", "Admin", new { area = "Admin" });
                else if (currentUser.Role == Role.Author) return RedirectToAction("Index", "Home", new { area = "Author" });
            }


            return View();
        }


        public ActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = db.GetByMail(HttpContext.User.Identity.Name);
                if (user.Role == Role.Member) return RedirectToAction("Index", "Home", new { area = "" });
                else if (user.Role == Role.Admin || user.Role == Role.Author) return RedirectToAction("Index", "Home", new { area = "Author" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserVM User)
        {
            var Validate = db.GetByExp(x => x.Email == User.Email || x.Nick == User.Nick);
            if (Validate.Count == 0)
            {
                AppUser apUser = new AppUser
                {
                    Nick = User.Nick,
                    Email = User.Email,
                    Password = User.Password,
                    isAllPropsFilled = false,
                    Role = Role.Member
                };

                db.Add(apUser);
                db.Save();
                return RedirectToAction("Login", "Account", new { area = "" });

            }
            else
            {
                ViewBag.Message = "Bu mail Adresi kullanılamaz";

                return View();
            }


        }



        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        [Role("Admin", "Author", "Member")]
        public ActionResult Like(int? id)
        {
            int userId = userDb.GetByName(HttpContext.User.Identity.Name).ID;
            Like like = new Like();

            if (!likeDb.Any(x => x.AppUserId == userId && x.ArticleID == id))
            {
                like.ArticleID = (int)id;
                like.AppUserId = userId;
                like.LikerName = userDb.GetById(userId).Nick;
                likeDb.Add(like);


            }
            else
            {

                like = likeDb.GetByExpSingle(x => x.ArticleID == id && x.AppUserId == userId);
                likeDb.Delete(like);
            }

            return Json(likeDb.GetByExp(x => x.ArticleID == id).Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotif()
        {
            int id = userDb.GetByName(HttpContext.User.Identity.Name).ID;
            List<Article> UserArticles = artdb.GetByExp(x => x.UserID == id);
            int likeCount = 0;
            foreach (var item in UserArticles)
            {
                 likeCount+= item.Likes.Count;
            }

            LikeDTO dt = new LikeDTO();
            dt.LikeCount = likeCount;
            return Json(dt, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Users(string id)
        {
            List<AppUser> user = userDb.GetAll();
            if (id != null)
            {
                user.Clear();
                user = userDb.GetByExp(x => x.Nick.Contains(id));
            }
                   return View(user);
        }

       


    }


}