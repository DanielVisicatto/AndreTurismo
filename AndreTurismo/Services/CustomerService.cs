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

        public int Create(Customer customer)
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
                                              "         ,Address) " +
                                      "    VALUES " +
                                              "(        @Name" +                                            
                                              "         ,@PhoneNumber" +
                                              "         ,@CellPhoneNumber" +
                                              "         ,@RegisterDate" +
                                              "         ,@Address);" +
                                              "         SELECT CAST(scope_identity() as int)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Name",              customer.Name));
                commandInsert.Parameters.Add(new SqlParameter("@PhoneNumber",       customer.PhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@CellPhoneNumber",   customer.CellPhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate",      customer.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Address",           customer.Address.Id));

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
        public List<Customer> GetAll() 
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
        public Customer GetById(int id)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("SELECT ");
                sb.Append("      cus.Id CustId ");
                sb.Append("      ,cus.Name CusName ");
                sb.Append("      ,cus.PhoneNUmber ");
                sb.Append("      ,cus.CellPhoneNumber ");
                sb.Append("      ,cus.RegisterDate CusReg ");
                sb.Append("      ,cus.Address CusAddress");

                sb.Append("      ,a.Id CusAddressId ");
                sb.Append("      ,a.Street CusAddrressStreet ");
                sb.Append("      ,a.Number CusAddressNumber ");
                sb.Append("      ,a.Neighborhood CusAddressNeighborhood ");
                sb.Append("      ,a.ZipCode CusAddressZipCode ");
                sb.Append("      ,a.Complement CusAddressComplement ");
                sb.Append("      ,a.City CusAddressCity ");
                sb.Append("      ,a.RegisterDate CusAddressReg ");

                sb.Append("      ,c.Id CusAddressCityId ");
                sb.Append("      ,c.Description CusAddressCityDescription ");
                sb.Append("      ,c.RegisterDate CusAddressCityReg ");

                sb.Append("      FROM [Customer] cus ");
                sb.Append("      JOIN [Address] a ");
                sb.Append("      ON cus.Address = a.Id ");
                sb.Append("      JOIN [City] c ");
                sb.Append("      ON a.City = c.Id ");
                sb.Append("      WHERE cus.Id = @Customer ");

                SqlCommand commandSelect = new SqlCommand(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Customer", id);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                if (dataReader.Read())
                {
                    Customer customer = new()
                    {
                        Id =                            (int)               dataReader["CustId"],
                        Name =                          (string)            dataReader["CusName"],
                        PhoneNumber =                   (string)            dataReader["PhoneNUmber"],
                        CellPhoneNumber =               (string)            dataReader["CellPhoneNumber"],
                        RegisterDate =                  (DateTime)          dataReader["CusReg"],
                        
                        Address = new()
                        {
                            Id =                        (int)               dataReader["CusAddressId"],
                            Street =                    (string)            dataReader["CusAddrressStreet"],
                            Number =                    (int)               dataReader["CusAddressNumber"],
                            Neighborhood =              (string)            dataReader["CusAddressNeighborhood"],
                            ZipCode =                   (string)            dataReader["CusAddressZipCode"],
                            Complement =                (string)            dataReader["CusAddressComplement"],
                            City = new()
                            {
                                Id =                    (int)               dataReader["CusAddressCityId"],
                                Description =           (string)            dataReader["CusAddressCityDescription"],
                                RegisterDate =          (DateTime)          dataReader["CusAddressCityReg"],
                            },
                            RegisterDate =              (DateTime)          dataReader["CusAddressReg"],
                        },                        

                    };
                    return customer;
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
        public List<Customer> GetByName(string name)
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

                sb.Append("   FROM [Customer] c");
                sb.Append("   JOIN [Address] a ");
                sb.Append("   ON c.Address = a.Id ");
                sb.Append("   JOIN [city] ct");
                sb.Append("   ON a.City = ct.Id ");
                sb.Append("   WHERE c.Name Like '%' + @Name + '%' ;");

                SqlCommand commandSelect = new(sb.ToString(), connection);
                commandSelect.Parameters.AddWithValue("@Name", name);

                SqlDataReader dataReader = commandSelect.ExecuteReader();

                while (dataReader.Read())
                {
                    Customer customer = new();

                    customer.Id =                           (int)               dataReader["CustomerId"];
                    customer.Name =                         (string)            dataReader["CustomerName"];
                    customer.PhoneNumber =                  (string)            dataReader["CustomerPhone"];
                    customer.CellPhoneNumber =              (string)            dataReader["CustomerCellPhone"];
                    customer.RegisterDate =                 (DateTime)          dataReader["CustomerRegister"];
                    customer.Address = new()
                    {
                        Id =                                (int)               dataReader["AddressId"],
                        Street =                            (string)            dataReader["AddressStreet"],
                        Number =                            (int)               dataReader["AddressNumber"],
                        Neighborhood =                      (string)            dataReader["AddressNeghb"],
                        ZipCode =                           (string)            dataReader["AddressZip"],
                        Complement =                        (string)            dataReader["AddressComp"],

                        City = new()
                        {
                            Id =                            (int)               dataReader["CityId"],
                            Description =                   (string)            dataReader["CityDesc"],
                            RegisterDate =                  (DateTime)          dataReader["CityReg"]
                        },
                        RegisterDate =                      (DateTime)          dataReader["AddressReg"]
                    };
                    customers.Add(customer);
                }
                return customers;

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
        public void Update(Customer customer)
        {
            try
            {
                connection.Open();

                StringBuilder sb = new();
                sb.Append("UPDATE [Customer] SET");
                sb.Append("       Name = @Name ");
                sb.Append("       ,PhoneNumber = @PhoneNumber ");
                sb.Append("       ,CellPhoneNumber = @CellPhoneNumber ");
                sb.Append("       ,Address = @Address ");
                sb.Append("       ,RegisterDate = @RegisterDate ");
              
                sb.Append("WHERE Id = @Id;");

                SqlCommand commandUpdate = new SqlCommand(sb.ToString(), connection);
                //commandUpdate.Parameters.AddWithValue("@Id", hotel.Id);

                commandUpdate.Parameters.Add(new SqlParameter("@Id", customer.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@Name", customer.Name));
                commandUpdate.Parameters.Add(new SqlParameter("@PhoneNumber", customer.PhoneNumber));
                commandUpdate.Parameters.Add(new SqlParameter("@CellPhoneNumber", customer.CellPhoneNumber));
                commandUpdate.Parameters.Add(new SqlParameter("@Address", customer.Address.Id));
                commandUpdate.Parameters.Add(new SqlParameter("@RegisterDate", customer.RegisterDate));
               ;


                int updated = commandUpdate.ExecuteNonQuery();
                if (updated == 0)
                {
                    Console.WriteLine($"Cliente de ID: {customer.Id} não existe.");
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

                string stringDelete = "DELETE FROM [Customer] WHERE Id = @Id";

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
