using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Net.Sockets;
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
                sb.Append("      t.Id                   TicketId ");
                sb.Append("      ,t.Home                TicketHomeAddress ");
                sb.Append("      ,t.Destiny             TicketDestinyAddress ");
                sb.Append("      ,t.Customer            TicketCustomer ");
                sb.Append("      ,t.Date ");
                sb.Append("      ,t.Price               TicketValor ");

                sb.Append("      ,ah.Id                 TicketHomeAddressId ");
                sb.Append("      ,ah.Street             TicketHomeAddressStreet ");
                sb.Append("      ,ah.Number             TicketHomeAddressNumber ");
                sb.Append("      ,ah.Neighborhood       TicketHomeAddressNeighborhood ");
                sb.Append("      ,ah.ZipCode            TicketHomeAddressZipCode ");
                sb.Append("      ,ah.Complement         TicketHomeAddressComplement ");
                sb.Append("      ,ah.City               TicketHomeAddressCity ");
                sb.Append("      ,ah.RegisterDate       TicketHomeAddressReg ");

                sb.Append("      ,ch.Id                 TicketAddressHomeCityId ");
                sb.Append("      ,ch.Description        TicketAddressHomeCityDescription ");
                sb.Append("      ,ch.RegisterDate       TicketAddressHomeCityCityReg ");

                sb.Append("      ,ad.Id                 TicketDestinyAddressId ");
                sb.Append("      ,ad.Street             TicketDestinyAddressStreet ");
                sb.Append("      ,ad.Number             TicketDestinyAddressNumber ");
                sb.Append("      ,ad.Neighborhood       TicketDestinyAddressNeighborhood ");
                sb.Append("      ,ad.ZipCode            TicketDestinyAddressZipCode ");
                sb.Append("      ,ad.Complement         TicketDestinyAddressComplement ");
                sb.Append("      ,ad.City               TicketDestinyAddressCity ");
                sb.Append("      ,ad.RegisterDate       TicketDestinyAddressReg ");

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

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                //commandSelect.Parameters.AddWithValue("@Ticket", );

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Ticket ticket = new()
                    {
                        Id =                                    (int)               dataReader["TicketId"],
                        Home = new()
                        {
                            Id =                                (int)               dataReader["TicketHomeAddressId "],
                            Street =                            (string)            dataReader["TicketHomeAddressStreet"],
                            Number =                            (int)               dataReader["TicketHomeAddressNumber "],
                            Neighborhood =                      (string)            dataReader["TicketHomeAddressNeighborhood "],
                            ZipCode =                           (string)            dataReader["TicketHomeAddressZipCode "],
                            Complement =                        (string)            dataReader["TicketHomeAddressComplement "],
                            City = new()
                            {
                                Id =                            (int)               dataReader["TicketAddressHomeCityId "],
                                Description =                   (string)            dataReader["TicketAddressHomeCityDescription "],
                                RegisterDate =                  (DateTime)          dataReader["TicketAddressHomeCityCityReg "],
                            },
                        },
                        Destiny = new()
                        {
                            Id =                                (int)               dataReader["TicketDestinyAddressId "],
                            Street =                            (string)            dataReader["TicketDestinyAddressStreet "],
                            Number =                            (int)               dataReader["TicketDestinyAddressNumber "],
                            Neighborhood =                      (string)            dataReader["TicketDestinyAddressNeighborhood "],
                            ZipCode =                           (string)            dataReader["TicketDestinyAddressZipCode "],
                            Complement =                        (string)            dataReader["TicketDestinyAddressComplement "],
                            City = new()
                            {
                                Id =                            (int)               dataReader["TicketAddressDestinyCityId "],
                                Description =                   (string)            dataReader["TicketAddressDestinyCityDescription "],
                                RegisterDate =                  (DateTime)          dataReader["TicketAddressDestinyCityReg "],
                            },
                            RegisterDate =                      (DateTime)          dataReader["TicketDestinyAddressReg "],
                        },
                        Customer = new()
                        {
                            Id =                                (int)               dataReader["TicketCustomerId"],
                            Name =                              (string)            dataReader["TicketCustomerName"],
                            PhoneNumber =                       (string)            dataReader["TicketCustomerPhoneNumber"],
                            CellPhoneNumber =                   (string)            dataReader["TicketCustomerCellPhoneNumber"],
                            RegisterDate =                      (DateTime)          dataReader["TicketCustomerRegisterDate"],

                            Address = new()
                            {
                                Id =                            (int)               dataReader["TicketCustomerAddressId"],
                                Street =                        (string)            dataReader["TicketCustomerAddressStreet"],
                                Number =                        (int)               dataReader["TicketCustomerAddressNumber"],
                                Neighborhood =                  (string)            dataReader["TicketCustomerAddressNeighborhood"],
                                ZipCode =                       (string)            dataReader["TicketCustomerAddressZipCode"],
                                Complement =                    (string)            dataReader["TicketCustomerAddressComplement "],
                                City = new()
                                {
                                    Id =                        (int)               dataReader["TicketCustomerAddressCityId "],
                                    Description =               (string)            dataReader["TicketCustomerAddressCityDescription "],
                                    RegisterDate =              (DateTime)          dataReader["TicketCustomerAddressCityReg "],
                                },
                                RegisterDate =                  (DateTime)          dataReader["TicketCustomerAddressRegisterDate "],
                            },
                        },
                        Date =                                  (DateTime)          dataReader["Date"],
                        Price =                                 (float)             dataReader["TicketValor"],
                    };
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
                sb.Append("      ,t.Price               TicketValor ");

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
