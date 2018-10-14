using System.Linq;
using GraduationTracker.Models;
using GraduationTracker.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit.Repositories
{
    [TestClass]
    public class StudentRepositoryTest
    {
        [TestMethod]
        public void TestGetStudentHit()
        {
            var student = StudentRepository.GetStudent(1);
            Assert.AreEqual(1, student.Id);

            var expectedCourses = new Course[]
            {
                new Course{Id = 1, Name = "Math", Mark=95 },
                new Course{Id = 2, Name = "Science", Mark=95 },
                new Course{Id = 3, Name = "Literature", Mark=95 },
                new Course{Id = 4, Name = "Physichal Education", Mark=95 }
            };

            foreach (var expectedCourse in expectedCourses)
            {
                var studentCourse = student.Courses
                        .Where(c => c.Id == expectedCourse.Id)
                        .First();

                Assert.AreEqual(expectedCourse.Mark, studentCourse.Mark);
                Assert.AreEqual(expectedCourse.Name, studentCourse.Name);
            }

            Assert.AreEqual(expectedCourses.Count(), student.Courses.Count());
        }

        [TestMethod]
        public void TestGetStudentMiss()
        {
            Assert.AreEqual(null, StudentRepository.GetStudent(500));
        }
    }
}
