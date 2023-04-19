using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    internal class AddressService
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
                string stringInsert = "INSERT INTO [Address] " +
                                              "(        Street" +
                                              "         ,Number" +
                                              "         ,Neighborhood" +
                                              "         ,ZipCode" +
                                              "         ,Complement" +
                                              "         ,City" +
                                              "         ,RegisterDate) " +
                                      "       VALUES " +
                                              "(        @Street" +
                                              "         ,@Number" +
                                              "         ,@Neighborhood" +
                                              "         ,@ZipCode" +
                                              "         ,@Complement" +
                                              "         ,@City" +
                                              "         ,@RegisterDate);" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);                

                commandInsert.Parameters.Add(new SqlParameter("@Street",            address.Street));
                commandInsert.Parameters.Add(new SqlParameter("@Number",            address.Number));
                commandInsert.Parameters.Add(new SqlParameter("@Neighborhood",      address.Neighborhood));
                commandInsert.Parameters.Add(new SqlParameter("@ZipCode",           address.ZipCode));
                commandInsert.Parameters.Add(new SqlParameter("@Complement",        address.Complement));
                commandInsert.Parameters.Add(new SqlParameter("@City",              address.City.Id));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",      address.RegisterDate));

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

                    address.Id =                    (int)               dataReader["AddressId"];
                    address.Street =                (string)            dataReader["AddressStreet"];
                    address.Number =                (int)               dataReader["AddressNumb"];
                    address.Neighborhood =          (string)            dataReader["AddressNeigh"];
                    address.ZipCode =               (string)            dataReader["AddressZip"];
                    address.Complement =            (string)            dataReader["AddressComp"];
                    address.City = new City()
                    {
                        Id =                        (int)               dataReader["CityId"],
                        Description =               (string)            dataReader["CityDesc"],
                        RegisterDate=               (DateTime)          dataReader["CityReg"]
                    };
                    address.RegisterDate =          (DateTime)          dataReader["AddresReg"];

                    addresses.Add(address);
                }
                return addresses;

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
