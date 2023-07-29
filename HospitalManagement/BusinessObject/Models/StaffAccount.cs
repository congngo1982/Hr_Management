using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class StaffAccount
    {
        public string HraccountId { get; set; }
        public string Hrfullname { get; set; }
        public string Hremail { get; set; }
        public string Hrpassword { get; set; }
        public int? StaffRole { get; set; }
    }
}
