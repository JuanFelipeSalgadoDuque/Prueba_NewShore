using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prueba_NewShore.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()
                                            .DeclaringType);

        [TestMethod]
        //MethodName_Parameters_ReturnSuccesOrError
        public void CreateFile_With_Oklist_ReturnsOK()
        {
            //init
            var list = new List<string>();
            list.Add("CARLOS");
            list.Add("FELIPE");
            list.Add("MARIA");
            Files instance = new Files(_Log);
            var privateTest = new PrivateObject(instance);

            //act
            var obj = privateTest.Invoke("CreateFile", list);

            //Assert
            Assert.IsNotNull(obj);
            Assert.AreEqual("File saving correctly", obj.ToString());
        }

        [TestMethod]
        public void ValidateFile_With_WrongExtension_ReturnErrorMessage()
        {
            //init
            Files instance = new Files(_Log);
            var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            var file = (HttpPostedFile)constructorInfo
                      .Invoke(new object[] { "filename.jpg", "image/jpeg", null });
            HttpPostedFileBase filebase = new HttpPostedFileWrapper(file);
            var expectedResult = "There was an error : Only file type with extension .txt is allowed";
            //act

            //Assert
            Assert.AreEqual(expectedResult, instance.ValidateFile(filebase));
        }

        [TestMethod]
        public void FindNames_With_OkParameters_ReturnList()
        {
            //init
            Files instance = new Files(_Log);
            string[] content = {"C", "R", "P", "A","P", "I", "E" };
            string[] registered = {"CARLOS", "PIPE" };
            var namesInContent = new List<string>();
            namesInContent.Add("CARLOS ---> Does not Exist");
            namesInContent.Add("PIPE ---> Exist");

            //act
            var result = instance.FindNames(content, registered);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(namesInContent.ToString(), result.ToString());
        }

        [TestMethod]
        public void FindNames_With_WrongParameters_ReturnNull()
        {
            //init
            Files instance = new Files(_Log);
            string[] content = { "C", "R", "P", "A", "P", "I", "E" };
            string[] registered = new string[0]; //empty array

            //act
            var result = instance.FindNames(content, registered);
            //Assert
            Assert.IsNull(result);
        }
    }
}
