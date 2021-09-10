using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H2Razor.Model;

namespace H2Razor.Repository
{
    public interface IRepository
    {

        public List<ToDo> GetAlltoDos();

        public void SaveAllToDos(List<ToDo> todo);

    }
}
