using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Pages
{
    public class HomeModel : PageModel
    {
        private readonly StaffService staffService;
        [BindProperty, Required(ErrorMessage = "Username is empty")]
        public string Username { get; set; }
        [BindProperty, Required(ErrorMessage = "Password is empty")]
        public string Password { get; set; }

        public HomeModel(StaffService service)
        {
            staffService = service;
        }

        public void OnGet(string errorMessage, string username, string password)
        {
            ViewData["ErrorMessage"] = errorMessage;
            Username = username;
            Password = password;
        }

        public IActionResult OnPost(string username, string password)
        {
            StaffAccount staff = staffService.GetById(username);
            if (staff == null) { return RedirectToPage(new { errorMessage = "Username is Incorrect !!!", username = username, password = password }); }
            if (password != staff.Hrpassword)
            {
                return RedirectToPage(new { errorMessage = "Password is Incorrect !!!", username = username, password = password });
            }
            if(staff.StaffRole != 0)
            {
                return RedirectToPage(new { errorMessage = "You cannot access to Resource !!!", username = username, password = password });
            }
            HttpContext.Session.SetString("account", staff.Hrfullname);
            return RedirectToPage("/Index");
        }

    }
}
