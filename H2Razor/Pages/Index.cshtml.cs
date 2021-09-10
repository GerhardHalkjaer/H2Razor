using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H2Razor.Model;
using H2Razor.Repository;

namespace H2Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IRepository _rep;

        public IndexModel(IRepository rep)
        {
            _rep = rep;
        }

        [BindProperty]
        public List<ToDo> toDoList { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //set todolist
        }
    }
}
