using log4net;
using Prueba_NewShore.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Prueba_NewShore.Controllers
{
    public class FileController : Controller
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()
                                            .DeclaringType);

        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase content, HttpPostedFileBase registered)
        {
            try
            {
                var result = new FileManagement(_Log);
                result.GetCustomers(content, registered);
                ViewBag.route = "RESULTADOS.txt file has been created in the Desktop ";
            }
            catch(Exception ex)
            {
                _Log.Error("There was an error: " + ex.Message);
                return View();
            }
            return View();
        }
    }
}