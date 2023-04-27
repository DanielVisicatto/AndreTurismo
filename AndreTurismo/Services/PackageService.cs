using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    internal class PackageService
    {
        readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        readonly SqlConnection connection;

        public PackageService()
        {
            connection = new SqlConnection(stringConnection);            
        }

        public int Create(Package package)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [Package] " +
                                              "(        Hotel" +
                                              "         ,Ticket" +
                                              "         ,RegisterDate" +
                                              "         ,Price" +
                                              "         ,Customer) " +
                                      "       VALUES " +
                                              "(        @Hotel" +
                                              "         ,@Ticket" +
                                              "         ,@RegisterDate" +
                                              "         ,@Price" +
                                              "         ,@Customer);" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                City city1 = new() { Description = "Araraquara", RegisterDate = DateTime.Now };

                commandInsert.Parameters.Add(new SqlParameter("@Hotel",         package.Hotel.Id));
                commandInsert.Parameters.Add(new SqlParameter("@Ticket",        package.Ticket.Id));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",  package.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Price",         package.Price));
                commandInsert.Parameters.Add(new SqlParameter("@Customer",      package.Customer.Id));


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

        public List<Package> GetAll()
        {
            try
            {
                connection.Open();
                List<Package> packages = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("        p.Id PackageId");
                sb.Append("       , p.Hotel ");
                sb.Append("       , p.Ticket ");
                sb.Append("       , p.RegisterDate PackageReg");
                sb.Append("       , p.Price PackagePrice");
                sb.Append("       , p.Customer ");

                sb.Append("       , h.Id IdHotel ");
                sb.Append("       , h.Name HotelName ");
                sb.Append("       , h.Address HotelAddress ");
                sb.Append("       , h.RegisterDate HotelReg ");
                sb.Append("       , h.Price HotelPrice ");

                sb.Append("       , ah.Id AddressHotelId ");
                sb.Append("       , ah.Street AddressHotelStreet ");
                sb.Append("       , ah.Number AddressHotelNumber ");
                sb.Append("       , ah.Neighborhood AddressHotelNeigh ");
                sb.Append("       , ah.ZipCode AddressHotelZip ");
                sb.Append("       , ah.Complement AddressHotelComplement ");
                sb.Append("       , ah.City AddressHotelCity ");
                sb.Append("       , ah.RegisterDate AddressHotelReg ");

                sb.Append("       , hac.Id HotelAddressCityId");
                sb.Append("       , hac.Description HotelAddressCityDesc");
                sb.Append("       , hac.RegisterDate HotelAddressCityReg");

                sb.Append("       , c.Id CustomerId ");
                sb.Append("       , c.Name CustomerName ");
                sb.Append("       , c.PhoneNumber CustomerPhone ");
                sb.Append("       , c.CellPhoneNumber CustomerCell ");
                sb.Append("       , c.RegisterDate CustomerReg");
                sb.Append("       , c.Address CustomerAddress ");

                sb.Append("       , ca.ID CustomerAddressId ");
                sb.Append("       , ca.Street CustomerAddressStreet ");
                sb.Append("       , ca.Number CustomerAddressNumb ");
                sb.Append("       , ca.Neighborhood CustomerAddressNeigh ");
                sb.Append("       , ca.ZiPcode CustomerAddressZip ");
                sb.Append("       , ca.Complement CustomerAddressCompl");
                sb.Append("       , ca.City CustomerCity ");
                sb.Append("       , ca.RegisterDate CustomerAddressReg ");

                sb.Append("       , cac.ID CustomerAddressCityId ");
                sb.Append("       , cac.Description CustomerAddressCityDescription ");
                sb.Append("       , cac.RegisterDate CustomerAddressCityReg ");

                sb.Append("   FROM [Package] p,");
                sb.Append("        [Hotel] h,");
                sb.Append("        [Address] ah,");
                sb.Append("        [City] hac,");
                sb.Append("        [Customer] c,");
                sb.Append("        [Address] ca,");
                sb.Append("        [City] cac ");
                sb.Append("        WHERE p.Hotel = h.Id AND ");
                sb.Append("        h.Address = ah.Id AND ");
                sb.Append("        ah.City = hac.Id AND");
                sb.Append("        ca.City = cac.Id");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Package package = new();

                    package.Id =                        (int)               dataReader["PackageId"];
                    package.Hotel = new Hotel()
                    {
                        Id =                            (int)               dataReader["IdHotel"],
                        Name =                          (string)            dataReader["HotelName"],
                        Address =                       new                 Address()
                        {
                            Id =                        (int)               dataReader["AddressHotelId"],
                            Street =                    (string)            dataReader["AddressHotelStreet"],
                            Number =                    (int)               dataReader["AddressHotelNumber"],
                            Neighborhood =              (string)            dataReader["AddressHotelNeigh"],
                            ZipCode =                   (string)            dataReader["AddressHotelZip"],
                            Complement =                (string)            dataReader["AddressHotelComplement"],
                            City = new City()
                            {
                                Id =                    (int)               dataReader["HotelAddressCityId"],
                                Description =           (string)            dataReader["HotelAddressCityDesc"],
                                RegisterDate =          (DateTime)          dataReader["HotelAddressCityReg"],
                            },
                            RegisterDate =              (DateTime)          dataReader["AddressHotelReg"]
                        },
                        RegisterDate =                  (DateTime)          dataReader["HotelReg"],
                        Price =                         (float)             dataReader["HotelPrice"],                        
                    };

                    package.RegisterDate =              (DateTime)          dataReader["PackageReg"];
                    package.Price =                     (float)            dataReader["PackagePrice"];
                    package.Customer =                  new                 Customer()
                    {
                        Id =                            (int)               dataReader["CustomerId"],
                        Name =                          (string)            dataReader["CustomerName"],
                        PhoneNumber =                   (string)            dataReader["CustomerPhone"],
                        CellPhoneNumber =               (string)            dataReader["CustomerCell"],
                        RegisterDate =                  (DateTime)          dataReader["CustomerReg"],
                        Address =                       new                 Address()
                        {
                            Id =                        (int)               dataReader["CustomerAddressId"],
                            Street =                    (string)            dataReader["CustomerAddressStreet"],
                            Number =                    (int)               dataReader["CustomerAddressNumb"],
                            Neighborhood =              (string)            dataReader["CustomerAddressNeigh"],
                            ZipCode =                   (string)            dataReader["CustomerAddressZip"],
                            Complement =                (string)            dataReader["CustomerAddressCompl"],
                            City =                      new                 City()
                            {
                                Id =                    (int)               dataReader["CustomerAddressCityId"],
                                Description =           (string)            dataReader["CustomerAddressCityDescription"],
                                RegisterDate =          (DateTime)          dataReader["CustomerAddressCityReg"],
                            },
                            RegisterDate =              (DateTime)          dataReader["CustomerAddressReg"],
                        },
                        
                    };
                    packages.Add(package);
                }
                return packages;
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
    }       
}
