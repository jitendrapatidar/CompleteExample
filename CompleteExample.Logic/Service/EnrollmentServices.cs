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
    public class EnrollmentServices : IEnrollmentServices, IDisposable
    {
        private CompleteExampleDBContext _context = null;
        private GenericRepository<Enrollment> _EnrollmentRepository;
        public EnrollmentServices()
        {
            _context = new CompleteExampleDBContext();
            _EnrollmentRepository = new GenericRepository<Enrollment>(_context);

        }

        

        #region Get

        // Get All Async
        public async Task<List<EnrollmentModel>> GetAllAsync()
        {

            List<EnrollmentModel> dto = new List<EnrollmentModel>();
            try
            {

                IEnumerable<Enrollment> obj = await _EnrollmentRepository.GetAllAsync();

                if (obj.Any())
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<Enrollment, EnrollmentModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<List<EnrollmentModel>>(obj);


                }
                else
                {
                    dto = new List<EnrollmentModel>();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message;

            }
            return dto;
        }

        // Get Async By Id
        public async Task<EnrollmentModel> GetByIdAsync(int Id)
        {
            EnrollmentModel dto = new EnrollmentModel();

            try
            {

                Enrollment obj = await _EnrollmentRepository.GetByIdAsync(Id);
                if (obj != null)
                {
                    
                    var configs = new MapperConfiguration(am => am.CreateMap<Enrollment, EnrollmentModel>());
                    var mapper = configs.CreateMapper();
                    dto = mapper.Map<EnrollmentModel>(obj);


                }
                else
                {
                    dto = new EnrollmentModel();
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
        public async Task<int> InsertAsync(EnrollmentModel source)
        {
            string Message = "";
            int NewId = 0;
            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("Enrollment");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<EnrollmentModel, Enrollment>());
                    var mapper = configs.CreateMapper();
                    Enrollment entity = mapper.Map<Enrollment>(source);
                     
                    await _EnrollmentRepository.InsertAsync(entity);
                    _EnrollmentRepository.CommitAsync();

                    
                    NewId = entity.EnrollmentId;


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
        public async Task<bool> UpdateAsync(EnrollmentModel source)
        {
            string Message = "";

            bool isUpdate = false;

            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("CountryMaster");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<EnrollmentModel, Enrollment>());
                    var mapper = configs.CreateMapper();

                    Enrollment entity = mapper.Map<Enrollment>(source);
                    await _EnrollmentRepository.UpdateAsync(entity);
                    _EnrollmentRepository.CommitAsync();
                    isUpdate = true;
                }
            }
            catch (Exception ex)
            {
                 Message = "Error: " + ex.Message;
                
            }
            return isUpdate;
        }
        public async Task<bool> UpdateAsync(EnrollmentModel source, int id)
        {

            string Message = "";

            bool isUpdate = false;
            try
            {
                if (source == null)
                {
                    var exp = new ArgumentNullException("Enrollment");
                    Message = "Error: " + exp;
                }
                else
                {
                    var configs = new MapperConfiguration(am => am.CreateMap<EnrollmentModel, Enrollment>());
                    var mapper = configs.CreateMapper();
                    Enrollment entity = mapper.Map<Enrollment>(source);
                    await _EnrollmentRepository.UpdateAsync(entity, id);
                    _EnrollmentRepository.CommitAsync();
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

    public interface IEnrollmentServices
    {
        Task<List<EnrollmentModel>> GetAllAsync();
        Task<EnrollmentModel> GetByIdAsync(int Id);
        Task<int> InsertAsync(EnrollmentModel source);

        Task<bool> UpdateAsync(EnrollmentModel source);
        Task<bool> UpdateAsync(EnrollmentModel source, int id);
    }




}
