using AndreTurismo.Models;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace Repositories
{
    public class AddressRepository : IAdressRepository
    {
        private string Connection { get; set; }

        public AddressRepository()
        {
            Connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public List<Address> GetAll()
        {
            using (var db = new SqlConnection(Connection))
            {
                db.Open();
                var addresses = db.Query<Address>(Address.GETALL);
                return (List<Address>)addresses;
            }
        }

        public bool Insert(Address address)
        {
            var status = false;
            using (var db = new SqlConnection(Connection))
            {
                db.Open();
                db.Execute(Address.INSERT, address);
                status = true;
            }
            return status;
        }
    }
}
