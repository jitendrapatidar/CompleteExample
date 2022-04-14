using System;
 

namespace CompleteExample.Data
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int InstructorId { get; set; }

    }
    

     
    public class GradesCourseModel
    {
        public int EnrollmentId { get; set; }
        public string fullname { get; set; }
        public string Coursename { get; set; }
        public int CourseId { get; set; }
        public Decimal Grade { get; set; }

    }

}
