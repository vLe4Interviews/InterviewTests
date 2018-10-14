using System;
using System.Collections.Generic;
using GraduationTracker.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        public void TestHasCredits()
        {
            var tracker = new GraduationTracker();

            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var students = new[]
            {
               new Student
               {
                   Id = 1,
                   Courses = new Course[]
                   {
                       new Course{Id = 1, Name = "Math", Mark=95 },
                       new Course{Id = 2, Name = "Science", Mark=95 },
                       new Course{Id = 3, Name = "Literature", Mark=95 },
                       new Course{Id = 4, Name = "Physichal Education", Mark=95 }
                   }
               },
               new Student
               {
                   Id = 2,
                   Courses = new Course[]
                   {
                       new Course{Id = 1, Name = "Math", Mark=80 },
                       new Course{Id = 2, Name = "Science", Mark=80 },
                       new Course{Id = 3, Name = "Literature", Mark=80 },
                       new Course{Id = 4, Name = "Physichal Education", Mark=80 }
                   }
               },
               new Student
               {
                   Id = 3,
                   Courses = new Course[]
                   {
                       new Course{Id = 1, Name = "Math", Mark=50 },
                       new Course{Id = 2, Name = "Science", Mark=50 },
                       new Course{Id = 3, Name = "Literature", Mark=50 },
                       new Course{Id = 4, Name = "Physichal Education", Mark=50 }
                   }
               },
                new Student
                {
                    Id = 4,
                    Courses = new Course[]
                    {
                        new Course{Id = 1, Name = "Math", Mark=40 },
                        new Course{Id = 2, Name = "Science", Mark=40 },
                        new Course{Id = 3, Name = "Literature", Mark=40 },
                        new Course{Id = 4, Name = "Physichal Education", Mark=40 }
                    }
                }
            };

            var graduatedResults = new List<Tuple<bool, Standing>>();

            foreach (var student in students)
            {
                graduatedResults.Add(tracker.HasGraduated(diploma, student));
            }


            // Assert.IsFalse(graduated.Any());
            // Tracker is always added. What I think should be tested is to
            // test every student against `tracker.HasGraduated` and see if the
            // the results are correct. Ideally they should each be it's own
            // test case.
            var expectedResult = new List<Tuple<bool, Standing>>()
            {
                new Tuple<bool, Standing>(true, Standing.SumaCumLaude),
                new Tuple<bool, Standing>(true, Standing.MagnaCumLaude),
                new Tuple<bool, Standing>(false, Standing.Average),
                new Tuple<bool, Standing>(false, Standing.Remedial),
            };

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], graduatedResults[i]);
            }
        }

        [TestMethod]
        public void TestStudentHasCreditsForSumaCumLaudeStandingAndCanGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=92 },
                    new Course{Id = 2, Name = "Science", Mark=95 },
                    new Course{Id = 3, Name = "Literature", Mark=95 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=98 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.SumaCumLaude, result.Item2);
            Assert.AreEqual(true, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForMagnaCumLaudeStandingAndCanGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=80 },
                    new Course{Id = 2, Name = "Science", Mark=81 },
                    new Course{Id = 3, Name = "Literature", Mark=85 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=90 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.MagnaCumLaude, result.Item2);
            Assert.AreEqual(true, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForAverageStandingAndCanGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=51 },
                    new Course{Id = 2, Name = "Science", Mark=51 },
                    new Course{Id = 3, Name = "Literature", Mark=51 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=51 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.Average, result.Item2);
            Assert.AreEqual(true, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForRemedialStanding()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=40 },
                    new Course{Id = 2, Name = "Science", Mark=40 },
                    new Course{Id = 3, Name = "Literature", Mark=30 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=60 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.Remedial, result.Item2);
            Assert.AreEqual(false, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForSumaCumLaudeStandingButCannotGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 2, Name = "Science", Mark=95 },
                    new Course{Id = 3, Name = "Literature", Mark=95 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=98 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.SumaCumLaude, result.Item2);
            Assert.AreEqual(false, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForMagnaCumLaudeStandingButCannotGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=80 },
                    new Course{Id = 2, Name = "Science", Mark=81 },
                    new Course{Id = 4, Name = "Physichal Education", Mark=90 }
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.MagnaCumLaude, result.Item2);
            Assert.AreEqual(false, result.Item1);
        }

        [TestMethod]
        public void TestStudentHasCreditsForAverageStandingButCannotGraduate()
        {
            var tracker = new GraduationTracker();
            var diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new int[] { 100, 102, 103, 104 }
            };

            var student = new Student
            {
                Id = 1,
                Courses = new Course[]
                {
                    new Course{Id = 1, Name = "Math", Mark=51 },
                    new Course{Id = 2, Name = "Science", Mark=51 },
                    new Course{Id = 3, Name = "Literature", Mark=51 },
                }
            };

            var result = tracker.HasGraduated(diploma, student);
            Assert.AreEqual(Standing.Average, result.Item2);
            Assert.AreEqual(false, result.Item1);
        }
    }
}
