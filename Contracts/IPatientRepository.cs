using CareSync.Common;
using CareSync.Data;
using CareSync.Data.Migrations;

namespace CareSync.Contracts
{

    public interface IPatientRepository : IBaseRepository<PatientPersonalInformation>
    {
        Task CreatePatientProfile(
            PatientPersonalInformation personalInfo,
            PatientHealthInformation healthInfo,
            PatientEmergencyContact emergencyContact);

        Task CreateConsultationInfo(int patientId, ConsultationDetail consultationDetail, ConsultationPrescription consultationPrescription);

        Task<PatientPersonalInformation> GetPatientInfo(int id);

        Task<ConsultationDetail> GetConsultationInfo(int id);

        Task<IEnumerable<PatientPersonalInformation>> GetAllPatientInfo();

        Task<IEnumerable<ConsultationDetail>> GetAllConsultationsInfo();

        Task<PaginatedResult<PatientPersonalInformation>> GetPaginatedPatient(int page, int pageSize, string keyword, string secondaryKeyword);

        Task<PaginatedResult<ConsultationDetail>> GetPaginatedConsultationsInfo(int page, int pageSize, string keyword, string secondaryKeyword);

        Task<IEnumerable<ConsultationDetail>> GetRecentConsultationsInfo();

        Task<IEnumerable<PatientPersonalInformation>> GetAll();

        Task UpdatePatientInfo(int id, PatientPersonalInformation incomingData);
    }
}
