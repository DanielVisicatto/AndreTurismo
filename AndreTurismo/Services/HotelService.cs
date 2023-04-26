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
            try
            {
                connection.Open();
                List<Hotel> hotels = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("        h.Id ");
                sb.Append("       , h.Name ");             
                sb.Append("       , h.Address ");             
                sb.Append("       , h.RegisterDate ");
                sb.Append("       , h.Price ");
                
                sb.Append("       , a.Id AddressHotelId ");
                sb.Append("       , a.Street AddressHotelStreet");
                sb.Append("       , a.NUmber AddressHotelNumber");
                sb.Append("       , a.Neighborhood AddressHotelNeighborhood");
                sb.Append("       , a.ZipCode AddressHotelZipCode");
                sb.Append("       , a.City AddressHotelCity");
                sb.Append("       , a.Complement AddressHotelComplement");
                sb.Append("       , a.RegisterDate AddressHotelReg");

                sb.Append("       , c.Id HotelCityId");
                sb.Append("       , c.Description HotelCityDescription");
                sb.Append("       , c.RegisterDate HotelCityReg");
                
                sb.Append("   FROM [Hotel] h");
                sb.Append("   ,[address] a");
                sb.Append("   ,[City] c");

                sb.Append("   WHERE h.Address = a.Id ");
                sb.Append("   AND a.City = c.Id ");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Hotel hotel = new();

                    hotel.Id =                          (int)           dataReader["Id"];
                    hotel.Name =                        (string)        dataReader["Name"];
                    hotel.Address = new Address()
                    {
                        Id =                            (int)           dataReader["AddressHotelId"], 
                        Street =                        (string)        dataReader["AddressHotelStreet"],
                        Number =                        (int)           dataReader["AddressHotelNumber"],
                        Neighborhood =                  (string)        dataReader["AddressHotelNeighborhood"],
                        ZipCode =                       (string)        dataReader["AddressHotelZipCode"],                        
                        Complement =                    (string)        dataReader["AddressHotelComplement"],

                        City = new City()
                        {
                            Id =                        (int)           dataReader["HotelCityId"],
                            Description =               (string)        dataReader["HotelCityDescription"],
                            RegisterDate =              (DateTime)      dataReader["HotelCityReg"],
                        },
                        RegisterDate =                  (DateTime)      dataReader["AddressHotelReg"]

                    };
                    hotel.RegisterDate =                (DateTime)      dataReader["RegisterDate"];
                    hotel.Price =                       (float)        dataReader["Price"];

                    hotels.Add(hotel);
                }
                return hotels;

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
        public Hotel Find(int id)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      h.Id HotelId ");
                sb.Append("      ,h.Name HotelName ");
                sb.Append("      ,h.RegisterDate HotelReg ");
                sb.Append("      ,h.Price HotelPrice ");

                sb.Append("      ,a.Id HotelAddressId ");
                sb.Append("      ,a.Street HotelAddrressStreet ");
                sb.Append("      ,a.Number HotelAddressNumber ");
                sb.Append("      ,a.Neighborhood HotelAddressNeighborhood ");
                sb.Append("      ,a.ZipCode HotelAddressZipCode ");
                sb.Append("      ,a.Complement HotelAddressComplement ");
                sb.Append("      ,a.City HotelAddressCity ");
                sb.Append("      ,a.RegisterDate HoteAddresslReg ");

                sb.Append("      ,c.Id HotelAddressCityId ");
                sb.Append("      ,c.Description HotelAddressCityDescription ");
                sb.Append("      ,c.RegisterDate HotelAddressCityReg ");

                sb.Append("      FROM [Hotel] h ");
                sb.Append("      JOIN [Address] a ");
                sb.Append("      ON h.Address = a.Id ");
                sb.Append("      JOIN [City] c ");
                sb.Append("      ON a.City = c.Id ");
                sb.Append("      WHERE h.Id = @Hotel ");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Hotel", id);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                if (dataReader.Read())
                {
                    Hotel hotel = new()
                    {
                        Id =                                (int)                   dataReader["HotelId"],
                        Name =                              (string)                dataReader["HotelName"],
                        Address = new()
                        {
                            Id =                            (int)                   dataReader["HotelAddressId"],
                            Street =                        (string)                dataReader["HotelAddrressStreet"],
                            Number =                        (int)                   dataReader["HotelAddressNumber"],
                            Neighborhood =                  (string)                dataReader["HotelAddressNeighborhood"],
                            ZipCode =                       (string)                dataReader["HotelAddressZipCode"],
                            Complement =                    (string)                dataReader["HotelAddressComplement"],
                            City = new()
                            {
                                Id =                        (int)                   dataReader["HotelAddressCityId"],
                                Description =               (string)                dataReader["HotelAddressCityDescription"],
                                RegisterDate =              (DateTime)              dataReader["HotelAddressCityReg"],
                            }, 
                            RegisterDate =                  (DateTime)              dataReader["HoteAddresslReg"],                            
                        },
                        RegisterDate =                      (DateTime)              dataReader["HotelReg"],
                        Price =                             (float)                 dataReader["HotelPrice"],

                    };
                    return hotel;
                }
                else
                {
                    return null!;
                }
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
        public List<Hotel> FindName(string name)
        {
            try
            {
                connection.Open();
                List<Hotel> hotels = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      h.Id HotelId ");
                sb.Append("      ,h.Name HotelName ");
                sb.Append("      ,h.RegisterDate HotelReg ");
                sb.Append("      ,h.Price HotelPrice ");

                sb.Append("      ,a.Id HotelAddressId ");
                sb.Append("      ,a.Street HotelAddrressStreet ");
                sb.Append("      ,a.Number HotelAddressNumber ");
                sb.Append("      ,a.Neighborhood HotelAddressNeighborhood ");
                sb.Append("      ,a.ZipCode HotelAddressZipCode ");
                sb.Append("      ,a.Complement HotelAddressComplement ");
                sb.Append("      ,a.City HotelAddressCity ");
                sb.Append("      ,a.RegisterDate HoteAddresslReg ");

                sb.Append("      ,c.Id HotelAddressCityId ");
                sb.Append("      ,c.Description HotelAddressCityDescription ");
                sb.Append("      ,c.RegisterDate HotelAddressCityReg ");

                sb.Append("      FROM [Hotel] h ");
                sb.Append("      JOIN [Address] a ");
                sb.Append("      ON h.Address = a.Id ");
                sb.Append("      JOIN [City] c ");
                sb.Append("      ON a.City = c.Id ");
                sb.Append("      WHERE h.Name LIKE '%' + @Name + '%' ;");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Name", name);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Hotel hotel = new()
                    {
                        Id =                                (int)           dataReader["HotelId"],
                        Name =                              (string)        dataReader["HotelName"],
                        Address = new()
                        {
                            Id =                            (int)           dataReader["HotelAddressId"],
                            Street =                        (string)        dataReader["HotelAddrressStreet"],
                            Number =                        (int)           dataReader["HotelAddressNumber"],
                            Neighborhood =                  (string)        dataReader["HotelAddressNeighborhood"],
                            ZipCode =                       (string)        dataReader["HotelAddressZipCode"],
                            Complement =                    (string)        dataReader["HotelAddressComplement"],
                            City = new()
                            {
                                Id =                        (int)           dataReader["HotelAddressCityId"],
                                Description =               (string)        dataReader["HotelAddressCityDescription"],
                                RegisterDate =              (DateTime)      dataReader["HotelAddressCityReg"],
                            },
                            RegisterDate =                  (DateTime)      dataReader["HoteAddresslReg"],
                        },
                        RegisterDate =                      (DateTime)      dataReader["HotelReg"],
                        Price =                             (float)         dataReader["HotelPrice"],
                       
                    };
                    hotels.Add(hotel);
                }
                return hotels;
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
        public void Update(Hotel hotel)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("UPDATE [Hotel] SET");
                sb.Append("       Name = @Name ");
                sb.Append("       ,Address = @Address ");
                sb.Append("       ,RegisterDate = @RegisterDate ");
                sb.Append("       ,Price = @Price ");
                sb.Append("WHERE Id = @Id;");

                SqlCommand commandUpdate = new SqlCommand(sb.ToString(), connection);
                //commandUpdate.Parameters.AddWithValue("@Id", hotel.Id);

                commandUpdate.Parameters.Add(new SqlParameter("@Id", hotel.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@Name", hotel.Name));
                commandUpdate.Parameters.Add(new SqlParameter("@Address", hotel.Address.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@RegisterDate", hotel.RegisterDate));
                commandUpdate.Parameters.Add(new SqlParameter("@Price", hotel.Price));


                int updated = commandUpdate.ExecuteNonQuery();
                if (updated == 0)
                {
                    Console.WriteLine($"Endereço de ID: {hotel.Id} não existe.");
                }
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
        public void Delete(int id)
        {
            try
            {
                connection.Open();

                string stringDelete = "DELETE FROM [Hotel] WHERE Id = @Id";

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
