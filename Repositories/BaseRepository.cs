using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CareSync.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {

        protected readonly ApplicationDbContext _dbcontext;
        protected readonly DbSet<T> _table;

        public BaseRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _table = _dbcontext.Set<T>();
        }

        public async Task Create(T t)
        {
            try
            {
                await _table.AddAsync(t);
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database error while saving data.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while creating the entity.", ex);
            }
        }

        public async Task<T> GetOne(object id)
        {
            var entity = await _table.FindAsync(id);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<PaginatedResult<T>> GetPaginated(int page, int pageSize, Expression<Func<T, bool>> condition, Expression<Func<T, bool>> secondCondition)
        {
            var count = await _table.Where(condition).Where(secondCondition).CountAsync();
            var records = await _table.Where(condition).Where(secondCondition).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<T>
            {
                Page = page,
                TotalCount = (int)Math.Ceiling(count / (double)pageSize),
                Result = records,
                TotalRecords = count
            };
        }

        public async Task Update(object id, object model)
        {
            var entity = await GetOne(id);
            if (entity != null)
            {
                _dbcontext.Entry(entity).CurrentValues.SetValues(model);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task Delete(object id)
        {
            var entity = await GetOne(id);
            if (entity != null)
            {
                _table.Remove(entity);
                await _dbcontext.SaveChangesAsync();
            }
        }

        //Specialized Repos
        //public async Task CreatePatientProfile(PatientPersonalInformation patientPersonalInformation, PatientHealthInformation patientHealthInformation, PatientEmergencyContact patientEmergencyContact)
        //{
        //    try
        //    {
        //        patientPersonalInformation.HealthInformation = patientHealthInformation;
        //        patientPersonalInformation.EmergencyContact = patientEmergencyContact;
        //        _dbcontext.AddAsync(patientPersonalInformation);
        //        await _dbcontext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        throw new Exception("A database error occurred while saving. Please check your data and try again.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An unexpected error occurred. Please try again or contact support.", ex);
        //    }


        //}
    }
}
