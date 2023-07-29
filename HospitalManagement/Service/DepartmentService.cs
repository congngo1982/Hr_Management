using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DepartmentService : RepositoryBase<Department>
    {
        public DepartmentService(HospitalManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
