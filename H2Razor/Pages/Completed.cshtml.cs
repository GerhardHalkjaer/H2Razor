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
    public class CompletedModel : PageModel
    {
        private readonly IRepository _rep;

        [BindProperty]
        public List<ToDo> toDoList { get; set; }

        public CompletedModel(IRepository rep)
        {
            _rep = rep;
        }

        public void OnGet()
        {
            toDoList = _rep.GetAlltoDos();

        }

        public void OnPost()
        {
            string id = Request.Form["id"];
        }

        public void OnPostSubmit()
        {

        }


    }
}
