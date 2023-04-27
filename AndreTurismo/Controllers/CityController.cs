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

        public int Create(City city)
        {
            return cityService.Create(city);
        }

        public List<City> GetAll()
        {
            return cityService.GetAll();
        }

        public List<City> GetByDesc(string description)
        {
            return cityService.GetByDesc(description);
        }

        public void Update (int id, City city)
        {
            cityService.Update(city);
        }

        public City GetById(int id)
        {
            return cityService.Get(id);
        }

        public void Delete (int id)
        {
            cityService.Delete(id);
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
