using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;

namespace HospitalManagement.Pages
{
    public class CreateModel : PageModel
    {
        //private readonly BusinessObject.Models.HospitalManagementDBContext _context;

        private readonly DoctorService doctorService;
        private readonly DepartmentService departmentService;
        public string User { get; set; }

        public CreateModel(DoctorService doctor, DepartmentService department)
        {
            doctorService = doctor;
            departmentService = department;
        }

        public IActionResult OnGet(string errorMessage)
        {
            string user = HttpContext.Session.GetString("account");
            if (user == null || user == "")
            {
                return RedirectToPage("/Home");
            }
            ViewData["ErrorMessage"] = errorMessage;
            ViewData["DepartmentId"] = new SelectList(departmentService.GetAll().ToList(), "DepartmentId", "DepartmentName");
            return Page();
        }

        [BindProperty]
        public DoctorInformation DoctorInformation { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string user = HttpContext.Session.GetString("account");
            if (user == null || user == "")
            {
                return RedirectToPage("/Home");
            }
            if (!ModelState.IsValid || doctorService.GetAll() == null || DoctorInformation == null)
            {
                return Page();
            }
            if(doctorService.GetById(DoctorInformation.DoctorId) != null)
            {
                return RedirectToPage(new { errorMessage = "Id is Existed !!!"});
            }
            doctorService.Add(DoctorInformation);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
