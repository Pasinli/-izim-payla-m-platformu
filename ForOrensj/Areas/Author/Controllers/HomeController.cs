using ForOrensj.Attributes;
using Oren.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForOrensj.Areas.Author.Controllers
{
    [Role("Author","Admin")]
    public class HomeController : Controller
    {
        MessageService msgdb = new MessageService();
        UserService userdb = new UserService();
        // GET: Author/Home

        
        public ActionResult Index()
        {
           
            return View();
        }
    }
}