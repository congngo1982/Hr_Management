using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BusinessObject.Models
{
    public partial class DoctorInformation
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string DoctorAddress { get; set; }
        public int? GraduationYear { get; set; }
        public string DoctorLicenseNumber { get; set; }
        public int? DepartmentId { get; set; }

        [JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
