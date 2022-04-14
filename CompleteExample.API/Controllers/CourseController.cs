using CompleteExample.Data;
using CompleteExample.Logic.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteExample.API.Controllers
{
    public class CourseController : Controller
    {
        // CourseServices : ICourseServices
        private readonly ICourseServices _courseService;

        public CourseController(ICourseServices  courseService)
        {
            _courseService =  courseService;
        }

        // GET: CourseController
        [HttpGet("GetCourseAll")]
        public async Task<IEnumerable<CourseModel>> Get()
        {

            return await _courseService.GetAllAsync();

        }
        // GET api/<CourseController>/5
        [HttpGet("CourseByid/{id}")]
        public async Task<CourseModel> Get(int id)
        {
            return await _courseService.GetByIdAsync(id);

        }
        [HttpGet("Grades/{rank}")]
        public async Task<IEnumerable<GradesCourseModel>> GetGradesByrank(int rank)
        {
            return await _courseService.GetGradesCourse(rank);
        }

    }
}
