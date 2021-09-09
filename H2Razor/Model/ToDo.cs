using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2Razor.Model
{
    public class ToDo
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string TaskDescription { get; set; }
        public Prio Priority { get; set; }
        public bool IsCompleted { get; set; }

        public ToDo()
        {
            CreatedTime = DateTime.Now;
        }

    }
    public enum Prio
    { low = 1,
        normal = 0,
        High = 2
    };

   


}
