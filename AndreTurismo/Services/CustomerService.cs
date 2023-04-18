﻿using AndreTurismo.Models;
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
            connection.Open();
        }

        public bool Insert(Customer customer)
        {
            bool status = false;
            try
            {
                string stringInsert = "INSERT INTO Customer " +
                                              "(        Name" +                                             
                                              "         ,PhoneNumber" +
                                              "         ,CellPhoneNumber" +
                                              "         ,Registerdate" +
                                              "         ,Id_Address) " +
                                      "VALUES " +
                                              "(        @Name" +                                            
                                              "         ,@PhoneNumber" +
                                              "         ,@CellPhoneNumber" +
                                              "         ,@Registerdate" +
                                              "         ,@Id_Address)";

                SqlCommand commandInsert = new SqlCommand(stringInsert, connection);

                commandInsert.Parameters.Add(new SqlParameter("@Name", customer.Name));
                commandInsert.Parameters.Add(new SqlParameter("@PhoneNumber", customer.PhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@CellPhoneNumber", customer.CellPhoneNumber));
                commandInsert.Parameters.Add(new SqlParameter("@Registerdate", customer.RegisterDate));
                commandInsert.Parameters.Add(new SqlParameter("@Id_Address", customer.Address));

                commandInsert.ExecuteNonQuery();
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
        public List<Customer> FindAll() 
        {
            List<Customer> customers = new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("       , c.Id ");
            sb.Append("       , c.Name ");
            sb.Append("       , c.PhoneNumber ");
            sb.Append("       , c.CellPhoneNumber ");
            sb.Append("       , c.RegisterDate ");
            sb.Append("       , c.Id_Address ");
            sb.Append("   FROM [Customer] c");

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();

            while (dataReader.Read())
            {
                Customer customer = new();

                customer.Id = (int)dataReader["Id"];
                customer.Name = (string)dataReader["Name"];
                customer.PhoneNumber = (string)dataReader["PhoneNumber"];
                customer.CellPhoneNumber = (string)dataReader["CellPhoneNumber"];
                customer.RegisterDate = (DateTime)dataReader["RegisterDate"];
                customer.Address = new Address()
                {
                    Street = (string)dataReader["Home"],
                    Number = (int)dataReader["Number"],
                    Neighborhood = (string)dataReader["Neighborhood"],
                    ZipCode = (string)dataReader["ZipCode"],
                    Complement = (string)dataReader["Complement"],

                    City = new City()
                    {
                        Description = (string)dataReader["Description"]
                    },
                    RegisterDate = (DateTime)dataReader["RegisterDate"]
                };
                customers.Add(customer);
            }
            return customers;
        }
    }
}