using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    internal class TicketService
    {
        readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        readonly SqlConnection connection;

        public TicketService()
        {
            connection = new SqlConnection(stringConnection);
            connection.Open();
        }

        public bool Insert(Ticket ticket)
        {
            bool status = false;
            try
            {
                string stringInsert = "INSERT INTO [Ticket] " +
                                              "(        Home" +
                                              "         ,Destiny" +
                                              "         ,Customer_Name" +
                                              "         ,Date" +
                                              "         ,Price) " +
                                      "    VALUES " +
                                              "(        @Home" +
                                              "         ,@Destiny" +
                                              "         ,@Customer_Name" +
                                              "         ,@Date" +
                                              "         ,@Price)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Home",          ticket.Home));
                commandInsert.Parameters.Add(new SqlParameter("@Desstiny",      ticket.Destiny));
                commandInsert.Parameters.Add(new SqlParameter("@Customer_Name", ticket.Customer));
                commandInsert.Parameters.Add(new SqlParameter("@Date",          ticket.Date));
                commandInsert.Parameters.Add(new SqlParameter("@Price",         ticket.Price));

                commandInsert.ExecuteNonQuery();
                status = true;

            }
            catch (Exception e)
            {
                status = false;
                throw new (e.Message);
            }
            finally
            {
                connection.Close();
            }
            return status;
            
        }

        public List<Ticket> FindAll()
        {
            List<Ticket> tickets = new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("       , t.Id ");
            sb.Append("       , t.Home HomeId ");
            sb.Append("       , t.Destiny DestinyId");
            sb.Append("       , t.Date TickekDate");
            sb.Append("       , t.Price TicketPrice");
                       
            sb.Append("       , h.Street HomeStreet");
            sb.Append("       , h.Number HomeNumber");
            sb.Append("       , h.ZipCode HomeZip");
            sb.Append("       , h.Complemet HomeComplement");
            sb.Append("       , h.City HomeCity");
            sb.Append("       , h.RegisterDate HomeDate");
            
            sb.Append("       , d.Street DestinyStreet");
            sb.Append("       , d.Number DestinyNumber");
            sb.Append("       , d.ZipCode DestinyZIpCode");
            sb.Append("       , d.Complemet DestinyComplement");
            sb.Append("       , d.City DestinyCity");
            sb.Append("       , d.RegisterDate DestinyRegister");

            sb.Append("       , ch.Description CityHomeDescription");
            sb.Append("       , cd.Description CityDestinyDescription");

            sb.Append("   FROM [Ticket] t," +
                      "   [Address ] h," +
                      "   [Address ] d," +
                      "   [City] ch," +
                      "   [City] cd" +
                      "WHERE t.Home = h.Id AND t.Destiny = d.Id AND h.City = ch.Id AND d.City = cd.Id");

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();

            while (dataReader.Read())
            {
                Ticket ticket = new();

                ticket.Id =                     (int)               dataReader["Id"];

                ticket.Home = new Address() 
                {
                    Street =                    (string)            dataReader["Home"],
                    Number =                    (int)               dataReader["Number"],
                    Neighborhood =              (string)            dataReader["Neighborhood"],
                    ZipCode =                   (string)            dataReader["ZipCode"],
                    Complement =                (string)            dataReader["Complement"],

                    City = new City() 
                    { 
                        Description =           (string)            dataReader["Description"] 
                    },
                    RegisterDate =              (DateTime)          dataReader["RegisterDate"]
                };

                ticket.Destiny = new Address()
                {
                    Street =                    (string)            dataReader["Street"],
                    Number =                    (int)               dataReader["Number"],
                    Neighborhood =              (string)            dataReader["Neighborhood"],
                    ZipCode =                   (string)            dataReader["ZipCode"],
                    Complement =                (string)            dataReader["Complement"],

                    City = new City() 
                    { 
                        Description =           (string)            dataReader["Description"] 
                    },
                    RegisterDate =              (DateTime)          dataReader["RegisterDate"]
                };

                ticket.Customer = new Customer()
                {
                    Name =                      (string)            dataReader["Name"]
                };

                ticket.Date =                   (DateTime)          dataReader["Date"];
                ticket.Price =                  (double)            dataReader["Price"];

                tickets.Add(ticket);
            }
            return tickets;
        }   
    }
}
