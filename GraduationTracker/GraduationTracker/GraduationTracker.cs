using System;
using System.Linq;
using GraduationTracker.Models;
using GraduationTracker.Repositories;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student)
        {
            // Credits is counted, but was never really used before.
            var credits = 0;
            var average = 0;

            foreach (var diplomaRequirement in diploma.Requirements)
            {
                var requirement = RequirementRepository.GetRequirement(diplomaRequirement);
                foreach (var requiredCourse in requirement.Courses)
                {
                    var courseTaken = student.Courses.Where(c => c.Id == requiredCourse);

                    // Assume course can be taken more than once, but at most
                    // one successful grade.
                    foreach (var course in courseTaken)
                    {
                        average += course.Mark;
                        if (course.Mark > requirement.MinimumMark)
                        {
                            credits += requirement.Credits;
                        }
                    }
                }
            }

            average = average / student.Courses.Length;

            var standing = Standing.None;
            var graduated = credits == diploma.Credits;

            if (average < 50)
            {
                graduated = false;
                standing = Standing.Remedial;
            }
            else if (average < 80)
            {
                standing = Standing.Average;
            }
            else if (average < 95)
            {
                standing = Standing.MagnaCumLaude;
            }
            else
            {
                standing = Standing.SumaCumLaude;
            }

            return new Tuple<bool, Standing>(graduated, standing);
        }
    }
}
