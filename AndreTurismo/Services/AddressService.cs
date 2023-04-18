﻿using AndreTurismo.Models;
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
            connection.Open();
        }

        public bool Insert(Address address)
        {
            bool status = false;
            try
            {
                string stringInsert = "INSERT INTO Address " +
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

                commandInsert.Parameters.Add(new SqlParameter("@Street", address.Street));
                commandInsert.Parameters.Add(new SqlParameter("@Number", address.Number));
                commandInsert.Parameters.Add(new SqlParameter("@Neighborhood", address.Neighborhood));
                commandInsert.Parameters.Add(new SqlParameter("@ZipCode", address.ZipCode));
                commandInsert.Parameters.Add(new SqlParameter("@Complement", address.Complement));
                commandInsert.Parameters.Add(new SqlParameter("@City", address.City));
                commandInsert.Parameters.Add(new SqlParameter("@RegisterDate", address.RegisterDate));

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
        public List<Address> FindAll()
        {
            List<Address> addresses = new();

            StringBuilder sb = new();
            sb.Append("SELECT ");
            sb.Append("       , a.Id ");
            sb.Append("       , a.Street ");
            sb.Append("       , a.Number ");
            sb.Append("       , a.Neighborhood ");
            sb.Append("       , a.ZipCode ");
            sb.Append("       , a.Complement ");
            sb.Append("       , a.City ");
            sb.Append("       , a.RegisterDate ");
            sb.Append("   FROM [Address] a");

            SqlCommand commandSelect = new(sb.ToString(), connection);
            SqlDataReader dataReader = commandSelect.ExecuteReader();

            while (dataReader.Read())
            {
                Address address = new();

                address.Id =                  (int)               dataReader["Id"];
                address.Street =              (string)            dataReader["Street"];
                address.Number =              (int)               dataReader["Number"];
                address.Neighborhood =        (string)            dataReader["Neighborhood"];
                address.ZipCode =             (string)            dataReader["ZipCode"];
                address.Complement =          (string)            dataReader["Complement"];
                address.City = new City() 
                {
                    Description =             (string)            dataReader["Description"]
                };
                address.RegisterDate =        (DateTime)          dataReader["RegisterDate"];

                addresses.Add(address);
            }
            return addresses;
        }
    }
}
