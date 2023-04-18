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
                string stringInsert = "INSERT INTO Ticket " +
                                              "(        Home" +
                                              "         ,Destiny" +
                                              "         ,Customer_Name" +
                                              "         ,Date" +
                                              "         ,Price) " +
                                      "VALUES " +
                                              "(        @Home" +
                                              "         ,@Destiny" +
                                              "         ,@Customer_Name" +
                                              "         ,@Date" +
                                              "         ,@Price)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Home", ticket.Home));
                commandInsert.Parameters.Add(new SqlParameter("@Desstiny", ticket.Destiny));
                commandInsert.Parameters.Add(new SqlParameter("@Customer_Name", ticket.Customer));
                commandInsert.Parameters.Add(new SqlParameter("@Date", ticket.Date));
                commandInsert.Parameters.Add(new SqlParameter("@Price", ticket.Price));

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
            sb.Append("       , t.Home ");
            sb.Append("       , t.Destiny ");
            sb.Append("       , t.Date ");
            sb.Append("       , t.Price ");
            sb.Append("   FROM [Ticket] t");

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
