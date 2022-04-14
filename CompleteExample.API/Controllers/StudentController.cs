
using CompleteExample.Data;
using CompleteExample.Logic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CompleteExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentService;

        public StudentController(IStudentServices studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudentAll")]
        public async Task<IEnumerable<StudentModel>> Get()
        {
       
            return await _studentService.GetAllAsync();
            
        }
        // GET api/<StudentController>/5
        [HttpGet("GetStudentByid/{id}")]
        public async Task<StudentModel> Get(int id)
        {
            return await _studentService.GetByIdAsync(id);
          
        }
        //For a particular instructor, list all the students' grades the instructor has given out
        [HttpGet("StudentsGradesAsyncByInstructorId/{id}")]
        public async Task<IEnumerable<StudentsGrades>> GetStudentsGradesAsyncByInstructorId(int id)
        {
            return await _studentService.GetStudentsGradesAsyncByInstructorId(id);
        }


        [HttpPost("CreateStudent")]
        [ValidateAntiForgeryToken]
        public async Task<int> PostCreateStudent(StudentModel source)
        {
            try
            {
                int id = await _studentService.InsertAsync(source);
                return id;
            }
            catch
            {
                return 0;
            }
        }


        [HttpPost("UpdateStudent")]
        public async Task<bool> PostUpdateStudent(StudentModel source)
        {
            bool isResult = await _studentService.UpdateAsync(source);
            return isResult;
        }



    }
}
