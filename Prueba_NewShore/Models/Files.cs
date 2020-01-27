using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Prueba_NewShore.Models
{
    public class Files : IFile
    {
        public void Result(HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            //private method for validate files

            throw new NotImplementedException();
        }


        //Receive a file and verify if it is valid and return a String with the contents of the file
        private string ValidateFile(HttpPostedFileBase file)
        {
            string FileName = file.FileName; //get file name
            string[] FileExtension = FileName.Split('.'); //get file extension

            if (!FileExtension[1].Equals("txt"))
            {
               return "There was an error : Only file type with extension .txt is allowed";
            }

            try
            {
                //Read all file
                var contentFile = new StreamReader(file.InputStream).ReadToEnd(); //returns string
                return contentFile;
            }
            catch (FileNotFoundException ex)
            {
                return "There was an error  : " + ex.Message;
            }
            catch (Exception ex)
            {
                return "There was an error  : " + ex.Message;
            }
        }

        
    }
}