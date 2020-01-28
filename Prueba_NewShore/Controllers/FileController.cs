using Prueba_NewShore.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Prueba_NewShore.Controllers
{
    public class FileController : Controller
    {

        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase content, HttpPostedFileBase registered)
        {
            var result = new Files();
            result.Result(content, registered);
            
            return View();
        }
    }
}