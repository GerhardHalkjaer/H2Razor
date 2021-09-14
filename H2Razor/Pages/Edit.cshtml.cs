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
        public ToDo todo { get; set; }

        public EditModel(IRepository rep)
        {
            _rep = rep;
        }

        public void OnGet(string id)
        {
            todo = _rep.GetAlltoDos().Find(x => x.Id == id);
            

        }

        public void OnPost(string id)
        {
            todo.IsCompleted = Request.Form["IsCompleted"].Equals("true");
            todo.TaskDescription = Request.Form["TaskDescription"];
            todo.Priority = (Prio)Convert.ToInt32(Request.Form["prio"]);
            todo.Id = Request.Form["id"];
            todo.CreatedTime = (DateTime)Request.Form["CreateDate"];
            _rep.UpdateToDo(todo);
        }

        public void OnPostEdit(string id)
        {

        }

        //public ActionResult OnPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
            




        //    return null;
        //}
    }
}
