using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Net.Http.Headers;
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
        }

        public int Insert(City city)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [City] " +
                                              "(        Description" +
                                              "         ,RegisterDate )" +
                                      "    VALUES " +
                                              "(        @Description" +
                                              "         ,@RegisterDate);" +
                                              "          SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Description", city.Description));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate", city.RegisterDate));

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
            try
            {
                connection.Open();
                List<City> cities = new();

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
                    City city = new();

                    city.Id = (int)dataReader["Id"];
                    city.Description = (string)dataReader["Description"];
                    city.RegisterDate = (DateTime)dataReader["RegisterDate"];

                    cities.Add(city);
                };
                return cities;
            }
            catch (Exception e)
            {
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<City> FindByDescription(string description)
        {
            try
            {
                connection.Open();
                List<City> cities = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      c.ID ");
                sb.Append("      ,c.Description ");
                sb.Append("      ,c.RegisterDate ");
                sb.Append("      FROM [City] c ");
                sb.Append("      WHERE c.Description LIKE '%' + @Description + '%'");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.Add(new SqlParameter("@Description", description));

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    City city = new();

                    city.Id = (int)dataReader["Id"];
                    city.Description = (string)dataReader["Description"];
                    city.RegisterDate = (DateTime)dataReader["RegisterDate"];

                    cities.Add(city);
                }
                return cities;
            }
            catch (Exception e)
            {
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateById(City city)
        {
            try
            {
                connection.Open();
                string stringUpdate = "UPDATE [City] SET" +
                                              "   Description = @Description" +
                                              "  ,RegisterDate = @RegisterDate" +
                                              "   WHERE Id = @Id";

                SqlCommand commandUpdate = new(stringUpdate, connection);

                commandUpdate.Parameters.Add(new SqlParameter("@Id", city.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@Description", city.Description));
                commandUpdate.Parameters.Add(new SqlParameter("@RegisterDate", city.RegisterDate));
                

                int updated = commandUpdate.ExecuteNonQuery();
                if (updated == 0)
                {
                    Console.WriteLine($"Cidade de ID: {city.Id} não foi encontrada.");
                }
                commandUpdate.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            try
            {
                connection.Open();

                string stringDelete = "DELETE FROM [City] WHERE Id = @Id";

                SqlCommand commandDelete = new SqlCommand(stringDelete, connection);
                commandDelete.Parameters.AddWithValue("@Id", id);

                commandDelete.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new (e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public City FindOne(int id)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      c.ID ");
                sb.Append("      ,c.Description ");
                sb.Append("      ,c.RegisterDate ");
                sb.Append("      FROM [City] c ");
                sb.Append("      WHERE c.Id = @Id");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.Add(new SqlParameter("@Id", id));

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                if (dataReader.Read())
                {
                    City city = new();
                    city.Id =                   (int)           dataReader["Id"];
                    city.Description =          (string)        dataReader["Description"];
                    city.RegisterDate =         (DateTime)      dataReader["RegisterDate"];
                    return city;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception e)
            {
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
    

