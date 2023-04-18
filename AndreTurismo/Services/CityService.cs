using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    public class CityService
    {
        static readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        static readonly SqlConnection connection;

        static CityService()
        {
            connection = new SqlConnection(stringConnection);
            connection.Open();
        }

        public int Insert(City city)
        {          
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [City] " +
                                              "(        Description" +
                                              "         ,RegisterDate )" +                                           
                                      "    VALUES " +
                                              "(        @Description" +
                                              "         ,@RegisterDate);"  +
                                              "          SELECT CAST(scope_identity() as int)"; 

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Description",       city.Description));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",      city.RegisterDate));                

                status = (int)commandInsert.ExecuteScalar();               

            }
            catch (Exception e)
            {
                
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return status;
        }

        public List<City> FindAll() 
        {
            List <City> cities= new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("       c.Id ");
            sb.Append("       ,c.Description ");
            sb.Append("       ,c.RegisterDate ");            
            sb.Append("   FROM [City] c");

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();
            
            while (dataReader.Read())
            {
                City city = new ();

                city.Id =                       (int)               dataReader["Id"];
                city.Description =              (string)            dataReader["Description"];
                city.RegisterDate =             (DateTime)          dataReader["RegisterDate"];
                
                cities.Add (city);
            };
            return cities;
        }
    }
}
