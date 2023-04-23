using AndreTurismo.Models;

namespace Repositories
{
    public interface IAdressRepository
    {
        bool Insert(Address address);
        List<Address> GetAll();
    }
}
