using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class CityController
    {
        private CityService cityService;

        public CityController()
        {
            cityService = new CityService();
        }

        public int Insert(City city)
        {
            return cityService.Insert(city);
        }

        public List<City> FindAll()
        {
            return cityService.FindAll();
        }

        public List<City> FindByDescription(string description)
        {
            return cityService.FindByDescription(description);
        }

        public void UpdateCity (int id, City city)
        {
            cityService.UpdateById(id, city);
        }

        public void DeleteCity (int id, City city)
        {
            cityService.Delete(id, city);
        }

        //public int InsertCity(City city)
        //{
        //    return new CityService().Insert(city);
        //}

        //public  List<City> FindAll()
        //{
        //    return new CityService().FindAll();
        //}
    }
}
