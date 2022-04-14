using System;
 

namespace CompleteExample.Data
{
  public  class StudentModel
    {
         
        public int? StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TimeZone { get; set; }
    }

    public class StudentsGrades
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }

        public Decimal Grade { get; set; }
    } 
}
