using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using University1.Controllers;

namespace University1.Tests
{
    [TestFixture]
    public class TestTeachersController
    {
        [Test]
        public void IndexViewResultNotNull()
        {
            TeachersController controller = new TeachersController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        public void DetailsViewResultNotNull()
        {
            TeachersController controller = new TeachersController();
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateViewResultNotNull()
        {
            TeachersController controller = new TeachersController();
            Teacher teacher = new Teacher();
            teacher.Name = "test";
            teacher.Telephone = "test";
            teacher.UserID = "test";
            var result = controller.Create(teacher);
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditViewResultNotNull()
        {
            TeachersController controller = new TeachersController();
            Teacher teacher = new Teacher();
            teacher.Name = "test";
            teacher.Telephone = "test";
            teacher.UserID = "test";
            var result = controller.Edit(teacher);
            Assert.IsNotNull(result);
        }
    }
}