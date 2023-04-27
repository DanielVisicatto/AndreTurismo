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
        }

        public int Create(Ticket ticket)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [Ticket] " +
                                              "(        Home" +
                                              "         ,Destiny" +
                                              "         ,Customer" +
                                              "         ,Date" +
                                              "         ,Price) " +
                                      "    VALUES " +
                                              "(        @Home" +
                                              "         ,@Destiny" +
                                              "         ,@Customer" +
                                              "         ,@Date" +
                                              "         ,@Price)" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Home",          ticket.Home.Id));
                commandInsert.Parameters.Add(new SqlParameter("@Destiny",       ticket.Destiny.Id));
                commandInsert.Parameters.Add(new SqlParameter("@Customer",      ticket.Customer.Id));
                commandInsert.Parameters.Add(new SqlParameter("@Date",          ticket.Date));
                commandInsert.Parameters.Add(new SqlParameter("@Price",         ticket.Price));

                status = (int)commandInsert.ExecuteScalar();
                

            }
            catch (Exception e)
            {                
                throw new (e.Message);
            }
            finally
            {
                connection.Close();
            }
            return status;
            
        }

        public List<Ticket> GetAll()
        {
            try
            {
                connection.Open();
                List<Ticket> tickets = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("        t.Id TicketId");
                sb.Append("       , t.Home HomeId ");
                sb.Append("       , t.Destiny DestinyId");
                sb.Append("       , t.Date TickekDate");
                sb.Append("       , t.Price TicketPrice");

                sb.Append("       , h.Street HomeStreet");
                sb.Append("       , h.Number HomeNumber");
                sb.Append("       , h.ZipCode HomeZip");
                sb.Append("       , h.Neighborhood HomeNeigh");
                sb.Append("       , h.Complement HomeComplement");
                sb.Append("       , h.City HomeCity");
                sb.Append("       , h.RegisterDate HomeReg");

                sb.Append("       , d.Street DestinyStreet");
                sb.Append("       , d.Number DestinyNumber");
                sb.Append("       , d.ZipCode DestinyZipCode");
                sb.Append("       , d.Neighborhood DestinyNeigh");
                sb.Append("       , d.Complement DestinyComplement");
                sb.Append("       , d.City DestinyCity");
                sb.Append("       , d.RegisterDate DestinyRegister");

                sb.Append("       , ch.Id CityHomeId");
                sb.Append("       , ch.Description CityHomeDescription");
                sb.Append("       , ch.RegisterDate CityHomeReg");
                sb.Append("       , cd.Id CityDestinyId");
                sb.Append("       , cd.Description CityDestinyDescription");
                sb.Append("       , cd.RegisterDate CityDestinyReg");

                sb.Append("       , c.Id TicketCustomerId");
                sb.Append("       , c.Name TicketCustomerName");
                sb.Append("       , c.PhoneNumber TicketCustomerPhoneNumber");
                sb.Append("       , c.CellPhoneNumber TicketCustomerCellPhoneNumber");
                sb.Append("       , c.RegisterDate TicketCustomerRegisterDate");
                sb.Append("       , c.Address TicketCustomerAddress");

                sb.Append("       , tca.Id TicketCustomerAddressId");
                sb.Append("       , tca.Street TicketCustomerStreet");
                sb.Append("       , tca.Number TicketCustomerNumber");
                sb.Append("       , tca.Neighborhood TicketCustomerNeighborhood");
                sb.Append("       , tca.ZipCode TicketCustomerZipCode");
                sb.Append("       , tca.Complement TicketCustomerComplement");
                sb.Append("       , tca.City TicketCustomerCity");
                sb.Append("       , tca.RegisterDate TicketCustomerRegisterDate");

                sb.Append("       , tcc.Id TicketCustomerCityId");
                sb.Append("       , tcc.Description TicketCustomerCityDescription");
                sb.Append("       , tcc.RegisterDate TicketCustomerCityRegisterDate");

                sb.Append("   FROM [Ticket] t," +
                          "   [Address] h," +
                          "   [Address] d," +
                          "   [Address] ca," +
                          "   [Address] tca," +
                          "   [Customer] c," +
                          "   [City] ch," +
                          "   [City] cd," +
                          "   [City] tcc" +
                          "    WHERE t.Home = h.Id " +
                          "    AND t.Destiny = d.Id " +
                          "    AND h.City = ch.Id " +
                          "    AND d.City = cd.Id " +
                          "    AND c.Address = tca.ID " +
                          "    AND tca.City = tcc.Id");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Ticket ticket = new();

                    ticket.Id =                 (int)               dataReader["TicketId"];
                    ticket.Home =               new                 Address()
                    {
                        Street =                (string)            dataReader["HomeStreet"],
                        Number =                (int)               dataReader["HomeNumber"],
                        Neighborhood =          (string)            dataReader["HomeNeigh"],
                        ZipCode =               (string)            dataReader["HomeZip"],
                        Complement =            (string)            dataReader["HomeComplement"],

                        City = new City()
                        {
                            Id =                (int)               dataReader["CityHomeId"],
                            Description =       (string)            dataReader["CityHomeDescription"],
                            RegisterDate =      (DateTime)          dataReader["CityHomeReg"],
                        },
                        RegisterDate =          (DateTime)          dataReader["HomeReg"]
                    };
                    ticket.Destiny =            new                 Address()
                    {

                        Street =                (string)            dataReader["DestinyStreet"],
                        Number =                (int)               dataReader["DestinyNumber"],
                        Neighborhood =          (string)            dataReader["DestinyNeigh"],
                        ZipCode =               (string)            dataReader["DestinyZipCode"],
                        Complement =            (string)            dataReader["DestinyComplement"],

                        City = new City()
                        {
                            Id =                (int)               dataReader["CityDestinyId"],
                            Description =       (string)            dataReader["CityDestinyDescription"],
                            RegisterDate =      (DateTime)          dataReader["CityDestinyReg"],
                        },
                        RegisterDate =          (DateTime)          dataReader["DestinyRegister"],
                    };
                    ticket.Customer =           new                 Customer()
                    {
                        Id =                    (int)               dataReader["TicketCustomerId"],
                        Name =                  (string)            dataReader["TicketCustomerName"],
                        PhoneNumber =           (string)            dataReader["TicketCustomerPhoneNumber"],
                        CellPhoneNumber =       (string)            dataReader["TicketCustomerCellPhoneNumber"],
                        RegisterDate =          (DateTime)          dataReader["TicketCustomerRegisterDate"],
                        Address = new Address()
                        {
                            Id =                (int)               dataReader["TicketCustomerAddressId"],
                            Street =            (string)            dataReader["TicketCustomerStreet"],
                            Number =            (int)               dataReader["TicketCustomerNumber"],
                            Neighborhood =      (string)            dataReader["TicketCustomerNeighborhood"],
                            ZipCode =           (string)            dataReader["TicketCustomerZipCode"],
                            Complement =        (string)            dataReader["TicketCustomerComplement"],
                            City =              new                 City()
                            {
                                Id =            (int)               dataReader["TicketCustomerCityId"],
                                Description =   (string)            dataReader["TicketCustomerCityDescription"],
                                RegisterDate =  (DateTime)          dataReader["TicketCustomerCityRegisterDate"],
                            },
                            RegisterDate = (DateTime)               dataReader["TicketCustomerRegisterDate"],
                        }
                    };
                    ticket.Date =               (DateTime)          dataReader["TickekDate"];
                    ticket.Price =              (float)             dataReader["TicketPrice"];

                    tickets.Add(ticket);
                }
                return tickets;
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
