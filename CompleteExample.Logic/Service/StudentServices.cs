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
    public class StudentServices : IStudentServices, IDisposable
    {
        private CompleteExampleDBContext _context = null;
        private GenericRepository<Student> _StudentRepository;
        private GenericRepository<StudentsGrades> _studentgradRepository;
        public StudentServices()
        {
            _context = new CompleteExampleDBContext();
            _StudentRepository = new GenericRepository<Student>(_context);
            _studentgradRepository = new GenericRepository<StudentsGrades>(_context);

        }

    
        #region Get

        // Get All Async
        public async Task<List<StudentModel>> GetAllAsync()
        {

            List<StudentModel> dto = new List<StudentModel>();
            try
            {

                IEnumerable<Student> obj = await _StudentRepository.GetAllAsync();

                if (obj.Any())
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<Student, StudentModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<List<StudentModel>>(obj);


                }
                else
                {
                    dto = new List<StudentModel>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        // Get Async By Id
        public async Task<StudentModel> GetByIdAsync(int Id)
        {
            StudentModel dto = new StudentModel();

            try
            {

                Student obj = await _StudentRepository.GetByIdAsync(Id);
                if (obj != null)
                {
                  
                    var configs = new MapperConfiguration(am => am.CreateMap<Student, StudentModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<StudentModel>(obj);


                }
                else
                {
                    dto = new StudentModel();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }
        public async Task<List<StudentsGrades>> GetStudentsGradesAsyncByInstructorId(int InstructorId)
        {
            string Query = "Select B.StudentId,Students.FirstName,B.Grade from  (select Enrollment.StudentId,Enrollment.Grade from(select CourseId from Courses C join  [Instructors] as I on i.InstructorId=C.InstructorId where I.InstructorId="+ InstructorId + ") as A join [Enrollment] on A.CourseId=Enrollment.CourseId) as B join Students on B.StudentId=Students.StudentId";
            List<StudentsGrades> dto = new List<StudentsGrades>();
            try
            {
                
                var obj = GenericRepository<StudentsGrades>.RawSqlQuery(Query, 
                    x => new StudentsGrades { StudentId = (int)x[0], FirstName = (string)x[1],Grade =(decimal)x[2] });//(Query);

                await Task.Delay(1);
                if (obj.Any())
                {
                    
                    dto = obj;


                }
                else
                {
                    dto = new List<StudentsGrades>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        #endregion

        #region Insert

        // Insert Model Async
        public async Task<int> InsertAsync(StudentModel source)
        {
            string Message = "";
            int NewId = 0;
            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("Student");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<StudentModel, Student>());
                    var mapper = configs.CreateMapper();
                    Student entity = mapper.Map<Student>(source);

                    await _StudentRepository.InsertAsync(entity);
                    _StudentRepository.CommitAsync();


                    NewId = entity.StudentId;


                }
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;

            }
            return NewId;
        }
        #endregion

        #region Update

        // Update Model Async
        public async Task<bool> UpdateAsync(StudentModel source)
        {
            string Message = "";

            bool isUpdate = false;

            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("Student");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<StudentModel, Student>());
                    var mapper = configs.CreateMapper();

                    Student entity = mapper.Map<Student>(source);
                    await _StudentRepository.UpdateAsync(entity);
                    _StudentRepository.CommitAsync();
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;

            }
            return isUpdate;
        }
        public async Task<bool> UpdateAsync(StudentModel source, int id)
        {

            string Message = "";

            bool isUpdate = false;
            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("Student");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<StudentModel, Student>());
                    var mapper = configs.CreateMapper();
                    Student entity = mapper.Map<Student>(source);
                    await _StudentRepository.UpdateAsync(entity, id);
                    _StudentRepository.CommitAsync();
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;

            }
            return isUpdate;
        }
        #endregion


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

    public interface IStudentServices
    {
        Task<List<StudentModel>> GetAllAsync();
        Task<StudentModel> GetByIdAsync(int Id);
      
        Task<List<StudentsGrades>> GetStudentsGradesAsyncByInstructorId(int InstructorId);
        Task<int> InsertAsync(StudentModel source);
        Task<bool> UpdateAsync(StudentModel source);
        Task<bool> UpdateAsync(StudentModel source, int id);


    }




}
