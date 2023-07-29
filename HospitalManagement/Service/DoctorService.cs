using BusinessObject.Models;

namespace Service
{
    public class DoctorService : RepositoryBase<DoctorInformation>
    {
        public DoctorService(HospitalManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
