

using CareSync.Common;
using CareSync.Data;
using System.Linq.Expressions;

namespace CareSync.Contracts
{
    public interface IBaseRepository<T>
    {
        Task Create(T t);

        Task<T> GetOne(object id);

        Task<IEnumerable<T>> GetAll();

        Task<PaginatedResult<T>> GetPaginated(int page, int pageSize, Expression<Func<T, bool>> condition, Expression<Func<T, bool>> secondCondition);

        Task Delete(object id);

        Task Update(object id, object model);

        //Specialized Repository

        //Task CreatePatientProfile(PatientPersonalInformation patientPersonalInformation, PatientHealthInformation patientHealthInformation, PatientEmergencyContact patientEmergencyContact);
    }
}
