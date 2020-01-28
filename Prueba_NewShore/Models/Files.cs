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
        private string ContentString { get; set; }
        private string RegisteredString { get; set; }
        private string[] ArrayContent { get; set; }
        private string[] ArrayRegistered { get; set; }

        public void Result(HttpPostedFileBase file1, HttpPostedFileBase file2)
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

        private List<string> FindNames(string[] content, string[] registered)
        {
            var contentList = content.ToList();
            List<string> nameInList = new List<string>();
            List<string> namesInContent = new List<string>();

            foreach (var name in registered)
            {
                foreach (var letter in new List<string>(contentList))
                {
                    if (name.Contains(letter))
                    {
                        nameInList.Add(letter);
                        contentList.Remove(letter);
                    }
                    if (name.Length == nameInList.Count())
                    {
                        namesInContent.Add(name + " ---> Exist");
                        nameInList.Clear();
                        break;
                    }
                }
                /*if (name.Length != nameInList.Count())
                {
                    namesInContent.Add(name + " --->Does not Exist");
                    nameInList.Clear();
                }*/
            }
            return namesInContent;
        }

        private string CreateFile(List<string> namesInContent)
        {
            try
            {
                string route = @"C:\Users\SALGADO\Desktop\RESULTADOS.txt";

                using (StreamWriter result = File.CreateText(route))//create the file
                {
                    foreach (string line in namesInContent)
                    {
                        result.WriteLine(line);
                    }
                    result.Flush();
                    result.Close();
                    return "File saving correctly";
                }
                
            }
            catch (Exception ex)
            {
                return "Error saving file" + ex.Message;
            }
        }
    }
}