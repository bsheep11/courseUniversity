using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using University1.Controllers;

namespace University1.Tests
{
    [TestFixture]
    public class TestCourseController
    {
        [Test]
            public void IndexViewResultNotNull()
            {
                CourseController controller = new CourseController();
                var result = controller.Index();
                Assert.IsNotNull(result);
            }


            [Test]
            public void DetailsViewResultNotNull()
            {
                CourseController controller = new CourseController();
                var result = controller.Details(1);
                Assert.IsNotNull(result);
            }

            [Test]
            public void CreateViewResultNotNull()
            {
                CourseController controller = new CourseController();
                Сourse course = new Сourse();
                course.Name = "test";
                course.TeacherID = 1;
                var result = controller.Create(course);
                Assert.IsNotNull(result);
            }

            [Test]
            public void EditViewResultNotNull()
            {
                CourseController controller = new CourseController();
                Сourse course = new Сourse();
                course.Name = "test";
                course.TeacherID = 1;
                var result = controller.Edit(course);
                Assert.IsNotNull(result);
            }
        }
}