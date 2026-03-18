using CareSync.Data;

namespace CareSync.Models
{
    public class PatientProfile
    {
        public PatientPersonalInformation PersonalInformation { get; set; }

        public PatientHealthInformation HealthInformation { get; set; }

        public PatientEmergencyContact EmergencyContact { get; set; }
    }
}
