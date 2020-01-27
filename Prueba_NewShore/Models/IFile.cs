using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Prueba_NewShore.Models
{
    interface IFile
    {
        void Result(HttpPostedFileBase file1, HttpPostedFileBase file2);
    }
}
