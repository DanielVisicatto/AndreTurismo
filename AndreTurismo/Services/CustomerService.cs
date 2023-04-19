using AndreTurismo.Models;
using System.Data.SqlClient;
using System.Text;

namespace AndreTurismo.Services
{
    internal class CustomerService
    {
        readonly string stringConnection = @"Server=(localdb)\MSSQLLocalDB;Integrated Security = true; AttachDbFileName=C:\Banco\AndreTurismo.mdf;";
        readonly SqlConnection connection;

        public CustomerService()
        {
            connection = new SqlConnection(stringConnection);            
        }

        public int Insert(Customer customer)
        {
            connection.Open();
            int status = 0;
            try
            {
                string stringInsert = "INSERT INTO [Customer] " +
                                              "(        Name" +                                             
                                              "         ,PhoneNumber" +
                                              "         ,CellPhoneNumber" +
                                              "         ,RegisterDate" +
                                              "         ,Id_Address) " +
                                      "    VALUES " +
                                              "(        @Name" +                                            
                                              "         ,@PhoneNumber" +
                                              "         ,@CellPhoneNumber" +
                                              "         ,@RegisterDate" +
                                              "         ,@Id_Address);" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Name",              customer.Name));
                commandInsert.Parameters.Add(new SqlParameter("@PhoneNumber",       customer.PhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@CellPhoneNumber",   customer.CellPhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",      customer.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Id_Address",        customer.Address.Id));

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

        public List<Customer> FindAll() 
        {
            try
            {
                connection.Open();
                List<Customer> customers = new();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("       c.Id CustomerId ");
                sb.Append("       , c.Name CustomerName");
                sb.Append("       , c.PhoneNumber CustomerPhone");
                sb.Append("       , c.CellPhoneNumber CustomerCellPhone");
                sb.Append("       , c.RegisterDate CustomerRegister");
                sb.Append("       , c.Address CustomerAddress");

                sb.Append("       , a.Id AddressId ");
                sb.Append("       , a.Street AddressStreet ");
                sb.Append("       , a.Number AddressNumber ");
                sb.Append("       , a.Neighborhood AddressNeghb ");
                sb.Append("       , a.ZipCode AddressZip ");
                sb.Append("       , a.Complement AddressComp ");
                sb.Append("       , a.City AddressCity ");
                sb.Append("       , a.RegisterDate AddressReg ");

                sb.Append("       , ct.Id CityId ");
                sb.Append("       , ct.Description CityDesc ");
                sb.Append("       , ct.RegisterDate CityReg ");
                sb.Append("   FROM [Customer] c,");
                sb.Append("   [Address] a,");
                sb.Append("   [city] ct");
                sb.Append("   WHERE c.Address = a.Id");
                sb.Append("   AND a.City = ct.Id");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Customer customer = new();

                    customer.Id =                   (int)               dataReader["CustomerId"];
                    customer.Name =                 (string)            dataReader["CustomerName"];
                    customer.PhoneNumber =          (string)            dataReader["CustomerPhone"];
                    customer.CellPhoneNumber =      (string)            dataReader["CustomerCellPhone"];
                    customer.RegisterDate =         (DateTime)          dataReader["CustomerRegister"];
                    customer.Address = new Address()
                    {
                        Id =                        (int)               dataReader["AddressId"],
                        Street =                    (string)            dataReader["AddressStreet"],
                        Number =                    (int)               dataReader["AddressNumber"],
                        Neighborhood =              (string)            dataReader["AddressNeghb"],
                        ZipCode =                   (string)            dataReader["AddressZip"],
                        Complement =                (string)            dataReader["AddressComp"],                        

                        City = new City()
                        {
                            Id =                    (int)               dataReader["CityId"],
                            Description =           (string)            dataReader["CityDesc"],
                            RegisterDate =          (DateTime)          dataReader["CityReg"]
                        },
                        RegisterDate =              (DateTime)          dataReader["AddressReg"]
                    };
                    customers.Add(customer);
                }
                return customers;

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
