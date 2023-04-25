using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    public class AddressService
    {
        readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        readonly SqlConnection connection;

        public AddressService()
        {
            connection = new SqlConnection(stringConnection);
        }
        public int Insert(Address address)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = " INSERT INTO [Address] " +
                                              "(         Street" +
                                              "         ,Number" +
                                              "         ,Neighborhood" +
                                              "         ,ZipCode" +
                                              "         ,Complement" +
                                              "         ,City" +
                                              "         ,RegisterDate) " +
                                      "       VALUES " +
                                              "(         @Street" +
                                              "         ,@Number" +
                                              "         ,@Neighborhood" +
                                              "         ,@ZipCode" +
                                              "         ,@Complement" +
                                              "         ,@City" +
                                              "         ,@RegisterDate);" +
                                              "SELECT CAST (scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                //commandInsert.Parameters.Add(new SqlParameter("@Id",                address.Id));
                commandInsert.Parameters.Add(new SqlParameter("@Street", address.Street));
                commandInsert.Parameters.Add(new SqlParameter("@Number", address.Number));
                commandInsert.Parameters.Add(new SqlParameter("@Neighborhood", address.Neighborhood));
                commandInsert.Parameters.Add(new SqlParameter("@ZipCode", address.ZipCode));
                commandInsert.Parameters.Add(new SqlParameter("@Complement", address.Complement));
                commandInsert.Parameters.Add(new SqlParameter("@City", address.City.Id));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate", address.RegisterDate));

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
        public List<Address> FindAll()
        {
            try
            {
                connection.Open();
                List<Address> addresses = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("       a.Id AddressId ");
                sb.Append("       , a.Street AddressStreet ");
                sb.Append("       , a.Number AddressNumb");
                sb.Append("       , a.Neighborhood AddressNeigh");
                sb.Append("       , a.ZipCode AddressZip");
                sb.Append("       , a.Complement AddressComp");
                sb.Append("       , a.City AddressCity");
                sb.Append("       , a.RegisterDate AddresReg");

                sb.Append("       , c.Id CityId");
                sb.Append("       , c.Description CityDesc");
                sb.Append("       , c.RegisterDate CityReg");

                sb.Append("   FROM [Address] a,");
                sb.Append("   [City] c");
                sb.Append("   WHERE a. City = c.Id");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Address address = new();

                    address.Id = (int)dataReader["AddressId"];
                    address.Street = (string)dataReader["AddressStreet"];
                    address.Number = (int)dataReader["AddressNumb"];
                    address.Neighborhood = (string)dataReader["AddressNeigh"];
                    address.ZipCode = (string)dataReader["AddressZip"];
                    address.Complement = (string)dataReader["AddressComp"];
                    address.City = new City()
                    {
                        Id = (int)dataReader["CityId"],
                        Description = (string)dataReader["CityDesc"],
                        RegisterDate = (DateTime)dataReader["CityReg"]
                    };
                    address.RegisterDate = (DateTime)dataReader["AddresReg"];

                    addresses.Add(address);
                }
                return addresses;

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
        public Address Find(int id)
        {            
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("     a.Id AddressId");
                sb.Append("     ,a.Street ");
                sb.Append("     ,a.Number ");
                sb.Append("     ,a.Neighborhood ");
                sb.Append("     ,a.ZipCode ");
                sb.Append("     ,a.Complement ");
                sb.Append("     ,a.City ");
                sb.Append("     ,a.RegisterDate AddressReg ");

                sb.Append("     ,c.Id CityId ");
                sb.Append("     ,c.Description CityDescription ");
                sb.Append("     ,c.RegisterDate CityReg ");

                sb.Append("     FROM [Address] a ");
                sb.Append("     JOIN [City] c ");
                sb.Append("     ON a.City = c.Id ");
                sb.Append("     WHERE a.Id = @Address;");                

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Address", id);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                if (dataReader.Read())
                {
                    Address address = new()
                    {
                        Id =                (int)                   dataReader["AddressId"],
                        Street =            (string)                dataReader["Street"],
                        Number =            (int)                   dataReader["Number"],
                        Neighborhood =      (string)                dataReader["Neighborhood"],
                        ZipCode =           (string)                dataReader["ZipCode"],
                        Complement =        (string)                dataReader["Complement"],
                        City = new()
                        {
                            Id =            (int)                   dataReader["CityId"],
                            Description =   (string)                dataReader["CityDescription"],
                            RegisterDate =  (DateTime)              dataReader["CityReg"],
                        },
                        RegisterDate =      (DateTime)              dataReader["AddressReg"],
                    };                    
                    return address;
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
        public void UpdateById(Address address)
        {
            try
            {
                connection.Open();

                string stringUpdate = "UPDATE [Address] SET " +
                                      "     Street = @Street " +
                                      "     ,Number = @Number " +
                                      "     ,Neighborhood = @Neighborhood " +
                                      "     ,ZipCode = @ZipCode " +
                                      "     ,Complement = @Complement " +
                                      "     ,City = @City " +
                                      "     ,RegisterDate = @RegisterDate " +                                  
                                      "WHERE Id = @Id; ";                                     

                SqlCommand commandUpdate = new (stringUpdate, connection);

                commandUpdate.Parameters.Add(new SqlParameter("@Id", address.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@Street", address.Street));
                commandUpdate.Parameters.Add(new SqlParameter("@Number", address.Number));
                commandUpdate.Parameters.Add(new SqlParameter("@Neighborhood", address.Neighborhood));
                commandUpdate.Parameters.Add(new SqlParameter("@ZipCode", address.ZipCode));
                commandUpdate.Parameters.Add(new SqlParameter("@Complement", address.Complement));
                commandUpdate.Parameters.Add(new SqlParameter("@City", address.City.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@RegisterDate", address.RegisterDate));

                //commandUpdate.Parameters.Add(new SqlParameter("@CityId", city.Id));
                //commandUpdate.Parameters.Add(new SqlParameter("@Description", city.Description));
                //commandUpdate.Parameters.Add(new SqlParameter("@CityRegisterDate", city.RegisterDate));

                int updated = commandUpdate.ExecuteNonQuery();
                if (updated == 0)
                {
                    Console.WriteLine($"Endereço de ID: {address.Id} não existe.");
                }
                commandUpdate.ExecuteNonQuery();

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
