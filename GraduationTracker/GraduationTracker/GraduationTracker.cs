using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;

            foreach (var requirementId in diploma.Requirements)
            {
                var requirement = Repository.GetRequirement(requirementId);
                foreach (var course in student.Courses)
                {
                    if (requirement.Courses.Any(c => c == course.Id))
                    {
                        average += course.Mark;
                        if (course.Mark >= requirement.MinimumMark)
                        {
                            credits += requirement.Credits;
                        }
                    }
                }
            }


            average = average / student.Courses.Length;

            var standing = STANDING.None;

            if (average < 50)
                standing = STANDING.Remedial;
            else if (average < 80)
                standing = STANDING.Average;
            else if (average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.MagnaCumLaude;




            if (credits >= diploma.Credits)
            {
                return new Tuple<bool, STANDING>(true, standing);
            }
            else
            {
                return new Tuple<bool, STANDING>(false, standing);
            }
        }
    }
}
