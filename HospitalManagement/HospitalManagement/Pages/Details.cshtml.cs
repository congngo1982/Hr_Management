using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HospitalManagement.Pages
{
    public class DetailsModel : PageModel
    {
        //private readonly BusinessObject.Models.HospitalManagementDBContext _context;
        private readonly DoctorService doctorService;
        private readonly DepartmentService departmentService;

        public DetailsModel(DoctorService doctor, DepartmentService department)
        {
            doctorService = doctor;
            this.departmentService = department;
        }

        public DoctorInformation DoctorInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string user = HttpContext.Session.GetString("account");
            if (user == null || user == "")
            {
                return RedirectToPage("/Home");
            }
            if (id == null || doctorService.GetAll().ToList() == null)
            {
                return NotFound();
            }

            var doctorinformation = doctorService.GetById(id);
            if (doctorinformation == null)
            {
                return NotFound();
            }
            else
            {
                DoctorInformation = doctorinformation;
            }
            return Page();
        }
    }
}
