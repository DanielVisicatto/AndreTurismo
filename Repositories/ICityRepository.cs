using AndreTurismo.Models;

namespace Repositories
{
    public interface ICityRepository
    {
        bool Insert(City city);
        List<City> GetAll();
    }
}
