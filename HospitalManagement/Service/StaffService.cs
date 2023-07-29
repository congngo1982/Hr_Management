using BusinessObject.Models;

namespace Service
{
    public class StaffService : RepositoryBase<StaffAccount>
    {
        public StaffService(HospitalManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
