using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    public class HotelService
    {
        readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        readonly SqlConnection connection;

        public HotelService()
        {
            connection = new SqlConnection(stringConnection);            
        }

        public int Insert(Hotel hotel)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [Hotel] " +
                                              "(        Name" +
                                              "         ,Address" +
                                              "         ,RegisterDate" +
                                              "         ,Price)" +
                                      "       VALUES " +
                                              "(        @Name" +
                                              "         ,@Address" +
                                              "         ,@RegisterDate" +
                                              "         ,@Price);" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Name",          hotel.Name));
                commandInsert.Parameters.Add(new SqlParameter("@Address",       hotel.Address.Id));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",  hotel.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Price",         hotel.Price));

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

        public List<Hotel> FindAll()
        {
            List<Hotel> hotels = new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("       , h.Id ");
            sb.Append("       , h.Hotel ");
            sb.Append("       , h.Ticket ");
            sb.Append("       , h.RegisterDate ");
            sb.Append("       , h.Price ");
            sb.Append("       , h.Customer ");
            sb.Append("   FROM [Hotel] h");

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();

            while (dataReader.Read())
            {
                Hotel hotel = new();

                hotel.Id =                          (int)               dataReader["Id"];
                hotel.Name =                        (string)            dataReader["Name"];
                hotel.Address = new Address()
                {

                    Street =                        (string)            dataReader["Home"],
                    Number =                        (int)               dataReader["Number"],
                    Neighborhood =                  (string)            dataReader["Neighborhood"],
                    ZipCode =                       (string)            dataReader["ZipCode"],
                    Complement =                    (string)            dataReader["Complement"],

                    City = new City()
                    {
                        Description =               (string)            dataReader["Description"]
                    },
                    RegisterDate =                  (DateTime)          dataReader["RegisterDate"]

                };               

                hotel.RegisterDate =                (DateTime)          dataReader["RegisterDate"];
                hotel.Price =                       (double)            dataReader["Price"];
                
                hotels.Add(hotel);
            }
            return hotels;
        }
    }
}
