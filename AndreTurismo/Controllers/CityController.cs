using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class CityController
    {
        public  int Insert(City city)
        {
            return new CityService().Insert(city);
        }

        public  List<City> FindAll()
        {
            return new CityService().FindAll();
        }
    }
}
