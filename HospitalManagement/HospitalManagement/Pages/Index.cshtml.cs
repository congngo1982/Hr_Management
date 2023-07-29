using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Service;
using System.ComponentModel.DataAnnotations;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly BusinessObject.Models.HospitalManagementDBContext _context;
        private readonly DoctorService doctorService;
        private readonly IConfiguration Configuration;
        [BindProperty, Required(ErrorMessage = "Enter value")]
        public string Name { get; set; }
        public string Sorting { get;set; }
        public string Staff { get; set; }
        public IndexModel(DoctorService service, IConfiguration configuration)
        {
            doctorService = service;
            Configuration = configuration;
        }

        //Paging
        public PaginatedList<DoctorInformation> Doctors { get; set; }

        public IList<DoctorInformation> DoctorInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string sorting, string name, int? pageIndex)
        {
            Name = name;
            if(Name == null)
            {
                HttpContext.Session.Remove("doctors");
            }
            if(sorting == null)
            {
                Sorting = "default";
            } else
            {
                Sorting = sorting;
            }
            string user = HttpContext.Session.GetString("account");
            if (user == null || user == "")
            {
                return RedirectToPage("/Home");
            }
            Staff = user;
            if (doctorService.GetAll() != null)
            {
                string doctor = HttpContext.Session.GetString("doctors");
                if(doctor != null) {
                    DoctorInformation = JsonSerializer.Deserialize<IList<DoctorInformation>>(doctor);
                }
                List <DoctorInformation> list = DoctorInformation == null ? doctorService.GetAll().Include(d => d.Department).ToList() : DoctorInformation.ToList();
                if (Sorting == "default")
                {
                    DoctorInformation = list;
                } else if(Sorting == "asc")
                {
                    //list = (from doctor in (doctorService.GetAll().Include(d => d.Department)) orderby doctor.DoctorName ascending select doctor).ToList();
                    DoctorInformation = (from d in list orderby d.DoctorName ascending select d).ToList();
                } else if(Sorting == "dec")
                {
                    //list = (from doctor in (doctorService.GetAll().Include(d => d.Department)) orderby doctor.DoctorName descending select doctor).ToList();
                    DoctorInformation = (from d in list orderby d.DoctorName descending select d).ToList();
                }
            }

            //Paging
            //var pageSize = Configuration.GetValue("PageSize", 4);
            ////IEnumerable<DoctorInformation> doctorList = from doc in DoctorInformation select doc;
            //Doctors = await PaginatedList<DoctorInformation>.CreateAsync(
            //    DoctorInformation.AsQueryable().AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }

        public void OnPost()
        {
            string user = HttpContext.Session.GetString("account");
            if (user != null && user != "")
            {
                Staff = user;
            }
            if (doctorService.GetAll() != null)
            {
                List<DoctorInformation> searchList = doctorService.GetAll().Include(d => d.Department).ToList();
                if (Name != null && Name.Trim() != "")
                {
                    //Name = name;
                    Console.WriteLine("Search: " + Name);
                    searchList = (from d in doctorService.GetAll().Include(d => d.Department) where d.DoctorName.Contains(Name) select d).ToList();
                    DoctorInformation = searchList;
                }else
                {
                    DoctorInformation = searchList;
                }
                string doctors = JsonSerializer.Serialize(DoctorInformation);
                HttpContext.Session.SetString("doctors", doctors);
            }
           
        }
    }
}
