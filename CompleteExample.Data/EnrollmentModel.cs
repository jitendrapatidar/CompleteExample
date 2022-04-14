using System;
 
namespace CompleteExample.Data
{
    public class EnrollmentModel
    {
   
        public int? EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Decimal Grade { get; set; }

    }
}
