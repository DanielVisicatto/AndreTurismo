using AndreTurismo.Models;

namespace Repositories
{
    public interface ICityRepository
    {
        int Create(City city);
        void Update(City city);
        void Delete(int id);
        List<City> ReadAll();
    }
}
