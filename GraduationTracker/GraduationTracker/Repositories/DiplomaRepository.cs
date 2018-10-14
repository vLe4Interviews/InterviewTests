using System.Linq;
using GraduationTracker.Models;

namespace GraduationTracker.Repositories
{
    public class DiplomaRepository
    {
        public static Diploma GetDiploma(int id)
        {
            var diplomas = GetDiplomas();
            return diplomas.Where(d => d.Id == id).FirstOrDefault();
        }

        private static Diploma[] GetDiplomas()
        {
            return new[]
            {
                new Diploma
                {
                    Id = 1,
                    Credits = 4,
                    Requirements = new int[]{100,102,103,104}
                }
            };
        }
    }
}
