using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using University1.Controllers;

namespace University1.Tests
{
    [TestFixture]
    public class TestStudentsController
    {

        [Test]
        public void IndexViewResultNotNull()
        {
            StudentsController controller = new StudentsController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        public void DetailsViewResultNotNull()
        {
            StudentsController controller = new StudentsController();
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateViewResultNotNull()
        {
            StudentsController controller = new StudentsController();
            Student student = new Student();
            student.Name = "test";
            student.Telephone = "test";
            student.Age = 1;
            student.UserID = "test";
            var result = controller.Create(student);
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditViewResultNotNull()
        {
            StudentsController controller = new StudentsController();
            Student student = new Student();
            student.Name = "test";
            student.Telephone = "test";
            student.Age = 1;
            student.UserID = "test";
            var result = controller.Edit(student);
            Assert.IsNotNull(result);
        }


    }
}

