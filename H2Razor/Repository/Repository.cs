using H2Razor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace H2Razor.Repository
{
    public class Repository : IRepository
    {
        public List<ToDo> GetAlltoDos()
        {
            if (File.Exists("todos.xml"))
            {
                List<ToDo> tempList;
                XmlSerializer serializer = new XmlSerializer(typeof(List<ToDo>));
                using ( Stream reader = new FileStream("todos.xml",FileMode.Open))
                {
                   tempList = (List<ToDo>)serializer.Deserialize(reader); 
                    
                }
                return tempList;
            }


            return null;
        }

        public void SaveAllToDos(List<ToDo> todo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ToDo>));
            using (Stream writer = new FileStream("todos.xml", FileMode.Create))
            {
                serializer.Serialize(writer, todo);
            }
        }
    }
}
