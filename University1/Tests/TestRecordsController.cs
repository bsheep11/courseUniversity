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
    public class TestRecordsController
    {
        [Test]
        public void IndexViewResultNotNull()
        {
            RecordsController controller = new RecordsController();
            var result = controller.Index();
            Assert.IsNotNull(result);
        }


        [Test]
        public void DetailsViewResultNotNull()
        {
            RecordsController controller = new RecordsController();
            var result = controller.Details(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateViewResultNotNull()
        {
            RecordsController controller = new RecordsController();
            Record record = new Record();
            record.StudentID = 1;
            record.CourseID = 1;
            var result = controller.Create(record);
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditViewResultNotNull()
        {
            RecordsController controller = new RecordsController();
            Record record = new Record();
            record.StudentID = 1;
            record.CourseID = 1;
            var result = controller.Edit(record);
            Assert.IsNotNull(result);
        }
    }
}