using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using H2Razor.Repository;
using H2Razor.Model;
namespace H2Razor.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository _rep;
        [BindProperty]
        public ToDo Todo { get; set; }

        public EditModel(IRepository rep)
        {
            _rep = rep;
        }

        public void OnGet(string id)
        {
            Todo = _rep.GetAlltoDos().Find(x => x.Id == id);
            

        }

        public IActionResult OnPost(string id)
        {
            Todo.IsCompleted = Request.Form["IsCompleted"].Equals("true");
            Todo.TaskDescription = Request.Form["TaskDescription"];
            Todo.Priority = (Prio)Convert.ToInt32(Request.Form["prio"]);
            Todo.Id = Request.Form["id"];
            Todo.CreatedTime = DateTime.Parse(Request.Form["CreateDate"]);
            Todo.SqlID = Convert.ToInt32(Request.Form["SqlId"]);
            
            _rep.UpdateToDo(Todo);


            return RedirectToPage("index");
        }

    }
}
