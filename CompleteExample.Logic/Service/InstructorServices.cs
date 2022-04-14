 
using CompleteExample.Entities;
using CompleteExample.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompleteExample.Data;

namespace CompleteExample.Logic.Service
{
    public class InstructorServices : IInstructorServices, IDisposable
    {
        private CompleteExampleDBContext _context = null;
        private GenericRepository<Instructor> _InstructorRepository;
        public InstructorServices()
        {
            _context = new CompleteExampleDBContext();
            _InstructorRepository = new GenericRepository<Instructor>(_context);

        }

      
        #region Get

        // Get All Async
        public async Task<List<InstructorModel>> GetAllAsync()
        {

            List<InstructorModel> dto = new List<InstructorModel>();
            try
            {

                IEnumerable<Instructor> obj = await _InstructorRepository.GetAllAsync();

                if (obj.Any())
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<Instructor, InstructorModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<List<InstructorModel>>(obj);


                }
                else
                {
                    dto = new List<InstructorModel>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        // Get Async By Id
        public async Task<InstructorModel> GetByIdAsync(int Id)
        {
            InstructorModel dto = new InstructorModel();

            try
            {

                Instructor obj = await _InstructorRepository.GetByIdAsync(Id);
                if (obj != null)
                {
                    //dto = Mapper.Map<TblCountryMaster, CountryMaster>(obj);
                    var configs = new MapperConfiguration(am => am.CreateMap<Instructor, InstructorModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<InstructorModel>(obj);


                }
                else
                {
                    dto = new InstructorModel();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
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

    public interface IInstructorServices
    {
        Task<List<InstructorModel>> GetAllAsync();
        Task<InstructorModel> GetByIdAsync(int Id);
    }




}
