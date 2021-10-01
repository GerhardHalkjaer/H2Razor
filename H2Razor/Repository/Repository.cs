using H2Razor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace H2Razor.Repository
{
    public class Repository : IRepository
    {

        private readonly SqlConnection con;

        public Repository()
        {
            IConfigurationRoot builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false).Build();
            string strCon = builder.GetConnectionString("ToDoDb");
            con = new SqlConnection(strCon);
        }

        public List<ToDo> GetAlltoDos()
        {

            SqlCommand cmd = new SqlCommand("dbo.spReadAllToDo", con);
            SqlDataReader dr;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
            List<ToDo> tempList = new List<ToDo>();

            con.Open();

            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ToDo newTodo = new ToDo();

                        newTodo.SqlID = dr.GetInt32(0);
                        newTodo.Id = dr.GetString(1);
                        newTodo.TaskDescription = dr.GetString(2);
                        newTodo.CreatedTime = dr.GetDateTime(3);
                        newTodo.Priority = (Prio)dr.GetInt32(4);
                        newTodo.IsCompleted = dr.GetInt32(5) == 1 ? true : false;

                        tempList.Add(newTodo);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }



            //if (File.Exists("todos.xml"))
            //{
            //    List<ToDo> tempList;
            //    XmlSerializer serializer = new XmlSerializer(typeof(List<ToDo>));
            //    using ( Stream reader = new FileStream("todos.xml",FileMode.Open))
            //    {
            //       tempList = (List<ToDo>)serializer.Deserialize(reader); 

            //    }
            //    return tempList;
            //}
            //else
            //{
            //    List<ToDo> _temp = new List<ToDo>();

            //    _temp.Add(new ToDo { Id = "0", CreatedTime = DateTime.Now, IsCompleted = false, Priority = Prio.normal, TaskDescription = "test data" });
            //    return _temp;
            //}

            return tempList;
        }

        public void SaveAllToDos(List<ToDo> todo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ToDo>));
            using (Stream writer = new FileStream("todos.xml", FileMode.Create))
            {
                serializer.Serialize(writer, todo);
            }
        }

        public void SaveToDo(ToDo todo)
        {
            // List<ToDo> toDos = GetAlltoDos();
            // toDos.Add(todo);
            // SaveAllToDos(toDos);
            SqlCommand cmd = new SqlCommand("dbo.spCreateToDo", con);
            SqlDataReader dr;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("Description",todo.TaskDescription);
            cmd.Parameters.AddWithValue("GUID",todo.Id);
            cmd.Parameters.AddWithValue("CreatedTime",todo.CreatedTime);
            cmd.Parameters.AddWithValue("prio",(int)todo.Priority);
            cmd.Parameters.AddWithValue("Status",todo.IsCompleted ? 1: 0);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }

        public void UpdateToDo(ToDo todo)
        {

            SqlCommand cmd = new SqlCommand("dbo.spUpdateToDo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ID",todo.SqlID);
            cmd.Parameters.AddWithValue("Prio", (int)todo.Priority);
            cmd.Parameters.AddWithValue("Desc", todo.TaskDescription);
            cmd.Parameters.AddWithValue("Status", todo.IsCompleted ? 1 : 0);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }


            //List<ToDo> allToDos = GetAlltoDos();
            //allToDos[allToDos.FindIndex(x => x.Id == todo.Id)] = todo;

            //SaveAllToDos(allToDos);

        }

        public void DeleteToDo(ToDo toDo)
        {
            SqlCommand cmd = new SqlCommand("dbo.spDeleteToDo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("ID", toDo.SqlID);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }

    }




    //#region ConnectionString
    //internal static IConfigurationRoot configuration { get; set; }
    //static string connectionString;
    //SqlConnection sqlCon = new SqlConnection(connectionString);
    //public static void SetConnectionString()
    //{
    //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
    //    configuration = builder.Build();
    //    connectionString = configuration.GetConnectionString("DefaultConnection");
    //}
    //#endregion
}
