using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace H2Razor.Model
{
    public class ToDo
    {
        public string Id { get; set; } //GUID
        public DateTime CreatedTime { get; set; }
        [Required,MaxLength(25)]
        public string TaskDescription { get; set; }
        public Prio Priority { get; set; }
        public bool IsCompleted { get; set; }
        public int SqlID { get; set; }

        public ToDo()
        {
            CreatedTime = DateTime.Now;
            IsCompleted = false;
            Id = Guid.NewGuid().ToString();
        }

    }
    public enum Prio
    { low = 1,
        normal = 0,
        High = 2
    };

   


}
