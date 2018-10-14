using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.Models;

namespace GraduationTracker.Repositories
{
    public class RequirementRepository
    {
        public static Requirement GetRequirement(int id)
        {
            var requirements = GetRequirements();
            return requirements.Where(r => r.Id == id).FirstOrDefault();
        }

        public static Requirement[] GetRequirements()
        {
            return new[]
            {
                new Requirement{Id = 100, Name = "Math", MinimumMark=50, Courses = new int[]{1}, Credits=1 },
                new Requirement{Id = 102, Name = "Science", MinimumMark=50, Courses = new int[]{2}, Credits=1 },
                new Requirement{Id = 103, Name = "Literature", MinimumMark=50, Courses = new int[]{3}, Credits=1},
                new Requirement{Id = 104, Name = "Physichal Education", MinimumMark=50, Courses = new int[]{4}, Credits=1 }
            };
        }
    }
}
