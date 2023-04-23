using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class AddressController
    {
        private AddressService addressService;

        public AddressController()
        {
            addressService = new AddressService();
        }

        public int Insert(Address address)
        {
            return addressService.Insert(address);
        }
        public List<Address> FindAll()
        {
            return addressService.FindAll();
        }

        //public int Insert(Address address)
        //{
        //    return new AddressService().Insert(address);
        //}

        //public List<Address> FindAll()
        //{
        //    return new AddressService().FindAll();
        //}
    }
}
