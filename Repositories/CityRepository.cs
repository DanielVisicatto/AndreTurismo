using AndreTurismo.Models;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace Repositories
{
    public class CityRepository : ICityRepository
    {
        private string _connection { get; set; }

        public CityRepository()
        {
            _connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }

        public List<City> ReadAll()
        {
            List<City> cities = new();
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                cities = db.Query<City>(City.GETALL).ToList();                
            }
            return cities;
        }

        public int Create(City city)
        {
            int id;
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                id = db.ExecuteScalar<int>(City.INSERT, city);                 
            }
            return id;
        }       

        public void Update(City city)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}