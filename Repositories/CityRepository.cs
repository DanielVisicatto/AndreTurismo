using AndreTurismo.Models;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace Repositories
{
    public class CityRepository : ICityRepository
    {
        private string Connection { get; set; }

        public CityRepository()
        {
            Connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public List<City> GetAll()
        {
            using (var db = new SqlConnection(Connection))
            {
                db.Open();
                var cities = db.Query<City>(City.GETALL);
                return (List<City>)cities;
            }
        }

        public bool Insert(City city)
        {
            var status = false;
            using (var db = new SqlConnection(Connection))
            {
                db.Open();
                db.Execute(Address.INSERT, city);
                status = true;
            }
            return status;
        }
    }
}