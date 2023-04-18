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
            connection.Open();
        }

        public bool Insert(Package package)
        {
            bool status = false;
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

                commandInsert.Parameters.Add(new SqlParameter("@Hotel",         package.Hotel));
                commandInsert.Parameters.Add(new SqlParameter("@Ticket",        package.Ticket.Id));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",  package.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Price",         package.Price));
                commandInsert.Parameters.Add(new SqlParameter("@Customer",      package.Customer));


                commandInsert.ExecuteScalar();
                status = true;

            }
            catch (Exception e)
            {
                status = false;
                throw new(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return status;
        }

        public List<Package> FindAll()
        {
            List<Package> packages = new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("        p.Id ");
            sb.Append("       , p.Hotel ");
            sb.Append("       , p.Ticket ");
            sb.Append("       , p.RegisterDate ");
            sb.Append("       , p.Price ");
            sb.Append("       , p.Customer ");

            sb.Append("       , h.Id IdHotel ");
            sb.Append("       , h.Name HotelName ");
            sb.Append("       , h.Address HotelAddress ");

            sb.Append("       , ah.Id HotelID ");
            sb.Append("       , ah.Street HotelStreet ");
            sb.Append("       , ah.Number HotelNumber ");
            sb.Append("       , ah.Neighborhood HotelNeighborhood ");
            sb.Append("       , ah.ZipCode HotelZipCode ");
            sb.Append("       , ah.Complement HotelComplement ");
            sb.Append("       , ah.City HotelCity ");

            sb.Append("       , c.Id CustomerId ");
            sb.Append("       , c.Name CustomerName ");
            sb.Append("       , c.PhoneNumber CustomerPhone ");
            sb.Append("       , c.CellPhoneNumber CustomerCell ");
            sb.Append("       , c.RegisterDate CustomerRegister ");
            sb.Append("       , c.Address CustomerAddress ");

            sb.Append("       , ca.Street CustomerStreet ");
            sb.Append("       , ca.Number CustomerAddressNumb ");
            sb.Append("       , ca.Neighborhood CustomerNeighborhood ");
            sb.Append("       , ca.ZiPcode CustomerZipCode ");
            sb.Append("       , ca.Complement CustomerComplement ");
            sb.Append("       , ca.City CustomerCity ");

            sb.Append("       , cc.Description CustomerCityDescription ");

            sb.Append("   FROM [Package] p,");
            sb.Append("        [Hotel] h,");
            sb.Append("        [Address] ah,");
            sb.Append("        [City] ch,");
            sb.Append("        [Customer] c,");
            sb.Append("        [Address] ca,");
            sb.Append("        [City] cc ");
            sb.Append("        WHERE p.Hotel = h.Id AND ");
            sb.Append("        h.Address = ah.Id AND ");
            sb.Append("        ah.City = cc.Id");            

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();

            while (dataReader.Read())
            {
                Package package = new();

                package.Id =                        (int)           dataReader["Id"];
                package.Hotel = new Hotel()
                {
                    Id =                            (int)           dataReader["Id"],
                    Name =                          (string)        dataReader["Name"],
                    Address = new Address()
                    {
                        Street =                    (string)        dataReader["Home"],
                        Number =                    (int)           dataReader["Number"],
                        Neighborhood =              (string)        dataReader["Neighborhood"],
                        ZipCode =                   (string)        dataReader["ZipCode"],
                        Complement =                (string)        dataReader["Complement"],

                        City = new City()
                        {
                            Description =           (string)        dataReader["Description"]
                        },
                        RegisterDate =              (DateTime)      dataReader["RegisterDate"]
                    }
                };

                package.RegisterDate =              (DateTime)      dataReader["RegisterDate"];
                package.Price =                     (double)        dataReader["Price"];
                package.Customer = new Customer()
                {
                    Id =                            (int)           dataReader["Id"],
                    Name =                          (string)        dataReader["Name"],
                    PhoneNumber =                   (string)        dataReader["PhoneNumber"],
                    CellPhoneNumber =               (string)        dataReader["CellPhoneNumber"],
                    RegisterDate =                  (DateTime)      dataReader["RegisterDate"],
                    Address = new Address()
                    {
                        Street =                    (string)        dataReader["Home"],
                        Number =                    (int)           dataReader["Number"],
                        Neighborhood =              (string)        dataReader["Neighborhood"],
                        ZipCode =                   (string)        dataReader["ZipCode"],
                        Complement =                (string)        dataReader["Complement"],

                        City = new City()
                        {
                            Description =           (string)        dataReader["Description"]
                        },
                        RegisterDate =              (DateTime)      dataReader["RegisterDate"]

                    }
                };
                packages.Add(package);
            }
            return packages;
        }
    }       
}
