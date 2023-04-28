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
        public Ticket GetById(int id)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      t.Id                   TicketId ");
                sb.Append("      ,t.Home                TicketHomeAddress ");
                sb.Append("      ,t.Destiny             TicketDestinyAddress ");
                sb.Append("      ,t.Customer            TicketCustomer ");
                sb.Append("      ,t.Date ");
                sb.Append("      ,t.Price               TicketValor");

                sb.Append("      ,ah.Id                 TicketHomeAddressId ");
                sb.Append("      ,ah.Street             TicketHomeAddrressStreet ");
                sb.Append("      ,ah.Number             TicketHomeAddressNumber ");
                sb.Append("      ,ah.Neighborhood       TicketHomeAddressNeighborhood ");
                sb.Append("      ,ah.ZipCode            TicketHomeAddressZipCode ");
                sb.Append("      ,ah.Complement         TicketHomeAddressComplement ");
                sb.Append("      ,ah.City               TicketHomeAddressCity ");
                sb.Append("      ,ah.RegisterDate       TicketHomeAddressReg ");

                sb.Append("      ,ad.Id                 TicketDestinyAddressId ");
                sb.Append("      ,ad.Street             TicketDestinyAddrressStreet ");
                sb.Append("      ,ad.Number             TicketDestinyAddressNumber ");
                sb.Append("      ,ad.Neighborhood       TicketDestinyAddressNeighborhood ");
                sb.Append("      ,ad.ZipCode            TicketDestinyAddressZipCode ");
                sb.Append("      ,ad.Complement         TicketDestinyAddressComplement ");
                sb.Append("      ,ad.City               TicketDestinyAddressCity ");
                sb.Append("      ,ad.RegisterDate       TicketDestinyAddressReg ");

                sb.Append("      ,ch.Id                 TicketAddressHomeCityId ");
                sb.Append("      ,ch.Description        TicketAddressHomeCityDescription ");
                sb.Append("      ,ch.RegisterDate       TicketAddressHomeCityCityReg ");

                sb.Append("      ,cd.Id                 TicketAddressDestinyCityId ");
                sb.Append("      ,cd.Description        TicketAddressDestinyCityDescription ");
                sb.Append("      ,cd.RegisterDate       TicketAddressDestinyCityReg ");

                sb.Append("      ,tcus.Id               TicketCustomerId ");
                sb.Append("      ,tcus.Name             TicketCustomerName ");
                sb.Append("      ,tcus.PhoneNumber      TicketCustomerPhoneNumber ");
                sb.Append("      ,tcus.CellPhoneNumber  TicketCustomerCellPhoneNumber ");
                sb.Append("      ,tcus.RegisterDate     TicketCustomerRegisterDate ");
                sb.Append("      ,tcus.Address          TicketCustomerAddress ");

                sb.Append("      ,tcusa.Id              TicketCustomerAddressId ");
                sb.Append("      ,tcusa.Street          TicketCustomerAddressStreet ");
                sb.Append("      ,tcusa.Number          TicketCustomerAddressNumber ");
                sb.Append("      ,tcusa.Neighborhood    TicketCustomerAddressNeighborhood ");
                sb.Append("      ,tcusa.ZipCode         TicketCustomerAddressZipCode ");
                sb.Append("      ,tcusa.Complement      TicketCustomerAddressComplement ");
                sb.Append("      ,tcusa.City            TicketCustomerAddressCity ");
                sb.Append("      ,tcusa.RegisterDate    TicketCustomerAddressRegisterDate ");

                sb.Append("      ,tcusac.Id             TicketCustomerAddressCityId ");
                sb.Append("      ,tcusac.Description    TicketCustomerAddressCityDescription ");
                sb.Append("      ,tcusac.RegisterDate   TicketCustomerAddressCityReg ");



                sb.Append("      FROM [Ticket] t ");
                sb.Append("         JOIN [Address] ah ");
                sb.Append("             ON t.Home = ah.Id ");

                sb.Append("         JOIN [Address] ad ");
                sb.Append("             ON t.Destiny = ad.Id ");

                sb.Append("         JOIN [City] ch ");
                sb.Append("             ON ah.City = ch.Id ");

                sb.Append("         JOIN [City] cd ");
                sb.Append("             ON ad.City = cd.Id ");


                sb.Append("         JOIN [Customer] tcus ");
                sb.Append("             ON t.Customer = tcus.Id ");

                sb.Append("         JOIN [Address] tcusa ");
                sb.Append("             ON tcus.Address = tcusa.Id ");

                sb.Append("         JOIN [City] tcusac ");
                sb.Append("             ON tcusa.City = tcusac.Id ");

                sb.Append("      WHERE t.Id = @Ticket ");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Ticket", id);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                if (dataReader.Read())
                {
                    Ticket ticket = new()
                    {
                        Id =                                (int)           dataReader["TicketId"],
                        Home = new()
                        {
                            Id =                            (int)           dataReader["TicketHomeAddressId "],
                            Street =                        (string)        dataReader["TicketHomeAddrressStreet"],
                            Number =                        (int)           dataReader["TicketHomeAddressNumber "],
                            Neighborhood =                  (string)        dataReader["TicketHomeAddressNeighborhood "],
                            ZipCode =                       (string)        dataReader["TicketHomeAddressZipCode "],
                            Complement =                    (string)        dataReader["TicketHomeAddressComplement "],
                            City = new()
                            {
                                Id =                        (int)           dataReader["TicketAddressHomeCityId "],
                                Description =               (string)        dataReader["TicketAddressHomeCityDescription "],
                                RegisterDate =              (DateTime)      dataReader["TicketAddressHomeCityCityReg "],
                            },
                        },
                        Destiny = new()
                        {
                            Id =                            (int)           dataReader["TicketDestinyAddressId "],
                            Street =                        (string)        dataReader["TicketDestinyAddrressStreet "],
                            Number =                        (int)           dataReader["TicketDestinyAddressNumber "],
                            Neighborhood =                  (string)        dataReader["TicketDestinyAddressNeighborhood "],
                            ZipCode =                       (string)        dataReader["TicketDestinyAddressZipCode "],
                            Complement =                    (string)        dataReader["TicketDestinyAddressComplement "],
                            City = new()
                            {
                                Id =                        (int)           dataReader["TicketAddressDestinyCityId "],
                                Description =               (string)        dataReader["TicketAddressDestinyCityDescription "],
                                RegisterDate =              (DateTime)      dataReader["TicketAddressDestinyCityReg "],
                            },
                            RegisterDate =                  (DateTime)      dataReader["TicketDestinyAddressReg "],
                        },
                        Customer = new()
                        {
                            Id =                            (int)           dataReader["TicketCustomerId"],
                            Name =                          (string)        dataReader["TicketCustomerName"],
                            PhoneNumber =                   (string)        dataReader["TicketCustomerPhoneNumber"],
                            CellPhoneNumber =               (string)        dataReader["TicketCustomerCellPhoneNumber"],
                            RegisterDate =                  (DateTime)      dataReader["TicketCustomerRegisterDate"],

                            Address = new()
                            {
                                Id =                        (int)           dataReader["TicketCustomerAddressId"],
                                Street =                    (string)        dataReader["TicketCustomerAddressStreet"],
                                Number =                    (int)           dataReader["TicketCustomerAddressNumber"],
                                Neighborhood =              (string)        dataReader["TicketCustomerAddressNeighborhood"],
                                ZipCode =                   (string)        dataReader["TicketCustomerAddressZipCode"],
                                Complement =                (string)        dataReader["TicketCustomerAddressComplement "],
                                City = new()
                                {
                                    Id =                    (int)           dataReader["TicketCustomerAddressCityId "],
                                    Description =           (string)        dataReader["TicketCustomerAddressCityDescription "],
                                    RegisterDate =          (DateTime)      dataReader["TicketCustomerAddressCityReg "],
                                },
                                RegisterDate =              (DateTime)      dataReader["TicketCustomerAddressRegisterDate "],
                            },
                        },
                        Date =                              (DateTime)      dataReader["Date"],
                        Price =                             (float)         dataReader["TicketValor"],
                    };
                    return ticket;
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
        public void Delete(int id)
        {
            try
            {
                connection.Open();

                string stringDelete = "DELETE FROM [Ticket] WHERE Id = @Id";

                SqlCommand commandDelete = new SqlCommand(stringDelete, connection);
                commandDelete.Parameters.AddWithValue("@Id", id);

                commandDelete.ExecuteNonQuery();
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
