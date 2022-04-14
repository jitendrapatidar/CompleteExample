using CompleteExample.Data;
using CompleteExample.Entities;
using CompleteExample.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace CompleteExample.Logic.Service
{
    public class CourseServices : ICourseServices, IDisposable
    {
        private CompleteExampleDBContext _context = null;
        private GenericRepository<Course> _CourseRepository;
        public CourseServices()
        {
            _context = new CompleteExampleDBContext();
            _CourseRepository = new GenericRepository<Course>(_context);

        }

 
        

        // Get All Async
        public async Task<List<CourseModel>> GetAllAsync()
        {

            List<CourseModel> dto = new List<CourseModel>();
            try
            {

                IEnumerable<Course> obj = await _CourseRepository.GetAllAsync();

                if (obj.Any())
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<Course, CourseModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<List<CourseModel>>(obj);


                }
                else
                {
                    dto = new List<CourseModel>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        // Get Async By Id
        public async Task<CourseModel> GetByIdAsync(int Id)
        {
            CourseModel dto = new CourseModel();

            try
            {

                Course obj = await _CourseRepository.GetByIdAsync(Id);
                if (obj != null)
                {
                    //dto = Mapper.Map<TblCountryMaster, CountryMaster>(obj);
                    var configs = new MapperConfiguration(am => am.CreateMap<Course, CourseModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<CourseModel>(obj);


                }
                else
                {
                    dto = new CourseModel();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }


        public async Task<List<GradesCourseModel>> GetGradesCourse(int rank)
        {
            string Query = ";WITH CTE AS (SELECT EnrollmentId,b.FirstName+''+b.LastName as fullname,c.Title as Coursename,a.CourseId, Grade, ROW_NUMBER() OVER(PARTITION BY a.CourseId ORDER BY Grade DESC) as rnk FROM Enrollment a inner join Students b on a.StudentId=b.StudentId inner join Courses c on a.CourseId=c.CourseId) SELECT EnrollmentId,fullname, Coursename,CourseId, Grade FROM CTE WHERE rnk <="+rank;

            List<GradesCourseModel> dto = new List<GradesCourseModel>();
            try
            {
                

                var obj = GenericRepository<GradesCourseModel>.RawSqlQuery(Query,
                    x => new GradesCourseModel {
                        EnrollmentId = (int)x[0], 
                        fullname = (string)x[1],
                        Coursename = (string)x[2],
                        CourseId = (int)x[3],
                        Grade = (decimal)x[4]});



                await Task.Delay(1);
                if (obj.Any())
                {

                    dto = obj;


                }
                else
                {
                    dto = new List<GradesCourseModel>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        #region private dispose variable declaration...
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            this.disposed = true;
        }

        #endregion
    }

    public interface ICourseServices
    {
        Task<List<CourseModel>> GetAllAsync();
        Task<CourseModel> GetByIdAsync(int Id);
        Task<List<GradesCourseModel>> GetGradesCourse(int rank);
    }




}
