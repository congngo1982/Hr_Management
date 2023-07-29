using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;

namespace HospitalManagement.Pages
{
    public class EditModel : PageModel
    {
        //private readonly BusinessObject.Models.HospitalManagementDBContext _context;
        private readonly DoctorService doctorService;
        private readonly DepartmentService departmentService;

        public EditModel(DoctorService doctor, DepartmentService department)
        {
            doctorService = doctor;
            this.departmentService = department;
        }

        [BindProperty]
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
            DoctorInformation = doctorinformation;
            ViewData["DepartmentId"] = new SelectList(departmentService.GetAll().ToList(), "DepartmentId", "DepartmentName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string user = HttpContext.Session.GetString("account");
            if (user == null || user == "")
            {
                return RedirectToPage("/Home");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(DoctorInformation).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DoctorInformationExists(DoctorInformation.DoctorId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            doctorService.Update(DoctorInformation);

            return RedirectToPage("./Index");
        }

        private bool DoctorInformationExists(string id)
        {
            return (doctorService.GetAll().ToList()?.Any(e => e.DoctorId == id)).GetValueOrDefault();
        }
    }
}
