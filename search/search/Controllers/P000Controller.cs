using search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace search.Controllers
{
    public class P000Controller : Controller
    {
        // GET: P000
        public ActionResult Index()
        {
            var db = new AppDbContext();
            var data = db.Products.Include(x => x.Category).ToList();
            return View(data);
        }
    }
}