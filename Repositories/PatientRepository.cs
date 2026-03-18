using CareSync.Common;
using CareSync.Contracts;
using CareSync.Data;
using Microsoft.EntityFrameworkCore;

namespace CareSync.Repositories
{
    public class PatientRepository : BaseRepository<PatientPersonalInformation>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task CreatePatientProfile(PatientPersonalInformation personalInfo, PatientHealthInformation healthInfo, PatientEmergencyContact emergencyContact)
        {
            try
            {
                personalInfo.HealthInformation = healthInfo;
                personalInfo.EmergencyContact = emergencyContact;

                _dbcontext.Add(personalInfo);

                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        // 1. Swap the 'PatientPersonalInformation' object for just the 'patientId'
        public async Task CreateConsultationInfo(int patientId, ConsultationDetail consultationDetail, ConsultationPrescription consultationPrescription)
        {
            try
            {
                consultationDetail.PatientPersonalInformationId = patientId;
                consultationPrescription.ConsultationDetail = consultationDetail;

                _dbcontext.Add(consultationDetail);
                _dbcontext.Add(consultationPrescription);


                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task<PatientPersonalInformation> GetPatientInfo(int id)
        {
            var patient = await _dbcontext.PatientPersonalInformations
                .Include(p => p.HealthInformation)
                .Include(p => p.EmergencyContact)
                .Include(p => p.ConsultationDetail)
                    .ThenInclude(c => c.ConsultationPrescription)
                .FirstOrDefaultAsync(p => p.Id == id);

            return patient;
        }

        public async Task<ConsultationDetail> GetConsultationInfo(int id)
        {
            var consultation = await _dbcontext.ConsultationDetails
            .Include(p => p.PatientInfo)
            .Include(p => p.ConsultationPrescription)
            // 2. Add the condition to find the matching ID
            .FirstOrDefaultAsync(p => p.Id == id);

            return consultation;
        }

        public async Task<IEnumerable<PatientPersonalInformation>> GetAllPatientInfo()
        {
            var patients = await _dbcontext.PatientPersonalInformations
            .Include(p => p.HealthInformation)
            .Include(p => p.EmergencyContact)
            .ToListAsync();

            return patients;
        }
        public async Task<IEnumerable<ConsultationDetail>> GetAllConsultationsInfo()
        {
            var allConsultations = await _dbcontext.ConsultationDetails
                .Include(c => c.PatientInfo)
                .Include(c => c.ConsultationPrescription)
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return allConsultations;
        }

        public async Task<PaginatedResult<PatientPersonalInformation>> GetPaginatedPatient(int page, int pageSize, string keyword, string secondaryKeyword)
        {
            return await GetPaginated(page, pageSize, t => t.FirstName.Contains(keyword ?? string.Empty) || t.LastName.Contains(keyword ?? string.Empty), t => t.ResidentialAddress.Contains(secondaryKeyword ?? string.Empty));
        }

        public async Task<PaginatedResult<ConsultationDetail>> GetPaginatedConsultationsInfo(int page, int pageSize, string keyword, string secondaryKeyword)
        {
            var count = await _dbcontext.ConsultationDetails.CountAsync();

            var records = await _dbcontext.ConsultationDetails
                .Where(t => t.PatientInfo.FirstName.Contains(keyword ?? string.Empty) || t.PatientInfo.LastName.Contains(keyword ?? string.Empty))
                .Where(t => t.DateofVisit.Contains(secondaryKeyword ?? string.Empty))
                .Include(c => c.PatientInfo)
                .Include(c => c.ConsultationPrescription)
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 3. Return the formatted result
            return new PaginatedResult<ConsultationDetail>
            {
                Page = page,
                TotalCount = (int)Math.Ceiling(count / (double)pageSize),
                Result = records,
                TotalRecords = count
            };
        }

        public async Task<IEnumerable<ConsultationDetail>> GetRecentConsultationsInfo()
        {
            var recentConsultations = await _dbcontext.ConsultationDetails
                .Include(c => c.PatientInfo)
                .Include(c => c.ConsultationPrescription)
                .OrderByDescending(c => c.Id)
                .Take(3) 
                .ToListAsync();

            return recentConsultations;
        }

        public async Task UpdatePatientInfo(int id, PatientPersonalInformation incomingData)
        {
            try
            {
                var existingPatient = await _dbcontext.PatientPersonalInformations
                    .Include(p => p.HealthInformation)
                    .Include(p => p.EmergencyContact)
                    .FirstOrDefaultAsync(p => p.Id == id);

                // If the patient doesn't exist, we THROW an error instead of returning false
                if (existingPatient == null)
                {
                    throw new KeyNotFoundException("The requested patient profile could not be found.");
                }

                // Map all the changes...
                existingPatient.FirstName = incomingData.FirstName;
                existingPatient.LastName = incomingData.LastName;
                existingPatient.DateofBirth = incomingData.DateofBirth;
                existingPatient.Gender = incomingData.Gender;
                existingPatient.ContactNumber = incomingData.ContactNumber;
                existingPatient.SecondaryPhoneNumber = incomingData.SecondaryPhoneNumber;
                existingPatient.ResidentialAddress = incomingData.ResidentialAddress;
                existingPatient.BloodType = incomingData.BloodType;
                existingPatient.UpdatedAt = DateTime.Now;

                if (existingPatient.HealthInformation != null && incomingData.HealthInformation != null)
                {
                    existingPatient.HealthInformation.Height = incomingData.HealthInformation.Height;
                    existingPatient.HealthInformation.Weight = incomingData.HealthInformation.Weight;
                    existingPatient.HealthInformation.KnownAllergies = incomingData.HealthInformation.KnownAllergies;
                    existingPatient.HealthInformation.MedicalCondition = incomingData.HealthInformation.MedicalCondition;
                    existingPatient.HealthInformation.UpdatedAt = DateTime.Now;
                }

                if (existingPatient.EmergencyContact != null && incomingData.EmergencyContact != null)
                {
                    existingPatient.EmergencyContact.ContactPersonName = incomingData.EmergencyContact.ContactPersonName;
                    existingPatient.EmergencyContact.EmergencyNumber = incomingData.EmergencyContact.EmergencyNumber;
                    existingPatient.HealthInformation.UpdatedAt = DateTime.Now;
                }

                // Save the changes
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("A database error occurred while saving.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
