using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Prueba_NewShore.Models
{
    public class Files : IFile
    {
        private string[] ArrayContent { get; set; }
        private string[] ArrayRegistered { get; set; }

        private readonly ILog _log;

        public Files(ILog log)
        {
            _log = log;
        }

        public void Result(HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            try
            {
                //private method for validate files
                //get the file string
                var contentString = ValidateFile(file1);
                var registeredString = ValidateFile(file2);

                //convert strings into arrays to be manipulated
                ArrayContent = Regex.Split(contentString, "\r\n");
                ArrayRegistered = Regex.Split(registeredString, "\r\n");

                //find names contents in file CONTENIDO.txt
                var namesInContentFIle = FindNames(ArrayContent, ArrayRegistered);

                //make a file with result
                var responseOfFile = CreateFile(namesInContentFIle);
            }
            catch (Exception ex)
            {
                _log.Error("Error in Result() in class Files: " + ex.Message);
            }
        }


        //Receive a file and verify if it is valid and return a String with the contents of the file
        private string ValidateFile(HttpPostedFileBase file)
        {
            string FileName = file.FileName; //get file name
            string[] FileExtension = FileName.Split('.'); //get file extension

            if (!FileExtension[1].Equals("txt"))
            {
                _log.Info("In ValidatingFile() in Files Class the extension of a File is wrong");
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
                _log.Error("Error in ValidatingFile() in class Files: " + ex.Message);
                return "There was an error  : " + ex.Message;
            }
            catch (Exception ex)
            {
                _log.Error("Error in ValidatingFile() in class Files: " + ex.Message);
                return "There was an error  : " + ex.Message;
            }
        }

        private string CreateFile(List<string> namesInContent)
        {
            try
            {
                //route = @"C:\Users\SALGADO\Desktop\RESULTADOS.txt";
                string route = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\RESULTADOS.txt";

                using (StreamWriter result = File.CreateText(route))//create the file
                {
                    foreach (string line in namesInContent)
                    {
                        result.WriteLine(line);
                    }
                    result.Flush();
                    result.Close();
                    _log.Info("File RESULTADOS.txt saving correctly");
                    return "File saving correctly";
                }

            }
            catch (Exception ex)
            {
                _log.Debug("Error saving file (CreateFile() in Files Class): " + ex.Message);
                return "Error saving file" + ex.Message;
            }
        }

        private List<string> FindNames(string[] content, string[] registered)
        {
            var contentList = content.ToList(); //convert string[] content into List
            List<string> nameInList = new List<string>();
            List<string> namesInContent = new List<string>();

            foreach (var name in registered)
            {
                char[] nameChar = name.ToCharArray();//converts name into to char[]

                foreach (var charac in nameChar)
                {
                    string findChar = contentList.FirstOrDefault(x => x.Equals(charac.ToString()));
                    if (findChar != null)
                    {
                        nameInList.Add(findChar);
                        contentList.Remove(findChar);
                    }
                }

                if (name.Length == nameInList.Count())
                {
                    namesInContent.Add(name + " ---> Exist");
                    nameInList.Clear();
                }
                else
                {
                    namesInContent.Add(name + " ---> Does not Exist");
                    nameInList.Clear();
                }
                if (name == registered.Last())
                {
                    break;
                }
            }
            return namesInContent;
        }

    }
}