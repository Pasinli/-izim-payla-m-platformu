using Oren.Service.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForOrensj.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ArticleService db = new ArticleService();

        public ActionResult Index(int? id,int page=1)
        {
            if(id!=null)
            {
                return View(db.GetAll().OrderByDescending(x => x.AddedDate).Where(x => x.UserID == id).ToPagedList(page, 6));
            }
            return View(db.GetAll().OrderByDescending(x=>x.AddedDate).ToPagedList(page,6));

        }

     


    }
}