using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using H2Razor.Model;
using H2Razor.Repository;

namespace H2Razor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository _rep;

        [BindProperty]
        public ToDo ToDo { get; set; }
        public Prio prio { get; set; }
        //TODO: priority

        public CreateModel(IRepository rep)
        {
            _rep = rep;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            else
            {
                _rep.SaveToDo(ToDo);
                return RedirectToPage("index");
            }
        }

    }
}
