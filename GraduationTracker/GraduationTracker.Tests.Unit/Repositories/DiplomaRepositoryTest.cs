using System.Linq;
using GraduationTracker.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit.Repositories
{
    [TestClass]
    public class DiplomaRepositoryTest
    {
        [TestMethod]
        public void TestGetDiplomaHit()
        {
            var diploma = DiplomaRepository.GetDiploma(1);
            Assert.AreEqual(1, diploma.Id);
            Assert.AreEqual(4, diploma.Credits);

            var expectedRequirements = new int[] { 100, 102, 103, 104 };
            foreach (var expectedRequirement in expectedRequirements)
            {
                Assert.IsTrue(diploma.Requirements.Contains(expectedRequirement));
            }

            Assert.AreEqual(expectedRequirements.Count(), diploma.Requirements.Count());
        }

        [TestMethod]
        public void TestGetDiplomaMiss()
        {
            Assert.AreEqual(null, DiplomaRepository.GetDiploma(500));
        }
    }
}
