using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NUnit.Framework;
using University1.Models;

namespace University1.Controllers
{
    [Authorize(Roles = "Admin, Teacher, Student")]
    public class TakeTestController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: TakeTest
        public ActionResult Index(int? idTest)
        {
            return RedirectToAction("TakeTest", new { idTest = idTest });
        }

        [HttpGet]
        public ActionResult TakeTest(int? idTest)
        {
            IQueryable<QuestionVM> questions = null;
            if (idTest != null)
                {
                    var testQuestions = db.TestQuestions.Include(t => t.Test)
                        .Where(t => t.TestID == idTest);
                    if (testQuestions.Count()==0) { return View("EmptyTest"); }
                    List<QuestionVM> list = new List<QuestionVM>();
                    foreach (var testQ in testQuestions)
                    {
                        ViewBag.testName = testQ.Test.TestName;
                    QuestionVM questionVm = new QuestionVM();
                        questionVm.QuestionID = testQ.Id;
                        questionVm.QuestionText = testQ.Question;
                        List<ChoiceVM> c = new List<ChoiceVM>();
                        c.Add(new ChoiceVM { ChoiceID = 1, ChoiceText = testQ.Ans2 });
                        c.Add(new ChoiceVM { ChoiceID = 2, ChoiceText = testQ.TrueAns });
                        if (testQ.Ans3 != null) { c.Add(new ChoiceVM { ChoiceID = 3, ChoiceText = testQ.Ans3 }); }
                        if (testQ.Ans4 != null) { c.Add(new ChoiceVM { ChoiceID = 4, ChoiceText = testQ.Ans4 }); }
                        questionVm.Choices = c;
                        list.Add(questionVm);
                    }
                    questions = list.AsQueryable();
                }
            return View(questions);
        }

        [HttpPost]
        public ActionResult TakeTest(List<QuizAnswersVM> resultQuiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<QuizAnswersVM>();
            bool pass = true;
            foreach (QuizAnswersVM answser in resultQuiz)
            {
                QuizAnswersVM result = db.TestQuestions.Include(t => t.Test)
                    .Where(q => q.Id == answser.QuestionID).Select(q => new QuizAnswersVM
                {
                    QuestionID = q.Id,
                    TestID = q.TestID,
                    isCorrect = (answser.AnswerQ.ToLower().Equals(q.TrueAns.ToLower()))
                    
                }).FirstOrDefault();
                if (result.isCorrect == false) { pass = false;}
                finalResultQuiz.Add(result);
            }

            if (pass == true)
            { try
                {
                    PassTheTest passTheTest = new PassTheTest();
                    passTheTest.TestID = finalResultQuiz[0].TestID;
                    Student student = Session["Student"] as Student;
                    passTheTest.StudentID = student.Id;
                    if (db.PassTheTest.FirstOrDefault(p =>
                            p.StudentID == passTheTest.StudentID & p.TestID == passTheTest.TestID) == null)
                    {
                        db.PassTheTest.Add(passTheTest);
                        db.SaveChanges();
                    }
                }
                catch (Exception e) { }
            }
            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }
    }
}