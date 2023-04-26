using AndreTurismo.Models;

namespace Repositories
{
    public interface IAdressRepository
    {
        int Create(Address address);
        List<Address> ReadAll();
        void Update(Address address);
        void Delete(Address address);

    }
}
