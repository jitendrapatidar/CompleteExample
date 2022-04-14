using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompleteExample.Data;
using CompleteExample.Logic.Service;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CompleteExample.API.Controllers
{
    public class EnrollmentController : Controller
    {

        private readonly IEnrollmentServices _enrollmentService;

       
        public EnrollmentController(IEnrollmentServices enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        // GET api/<EnrollmentController> 
        [HttpGet("GetEnrollmentAll")]
        public async Task<IEnumerable<EnrollmentModel>> Get()
        {
            return await _enrollmentService.GetAllAsync();

        }
        // GET api/<EnrollmentController>/5
        [HttpGet("GetEnrollmentByid/{id}")]
        public async Task<EnrollmentModel> Get(int id)
        {
            return await _enrollmentService.GetByIdAsync(id);
        }

        // POST: EnrollmentController/EnrollStudent
        [HttpPost("EnrollStudent")]
        [ValidateAntiForgeryToken]
        public async Task<int> PostEnrollStudent(EnrollmentModel source)
        {
            try
            {
                int id = await _enrollmentService.InsertAsync(source);
                return id;
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost("UpdateStudent")]
        public async Task<bool> PostUpdateStudent(EnrollmentModel source)
        {
            bool isResult = await _enrollmentService.UpdateAsync(source);
            return isResult;
        }
    }
}
