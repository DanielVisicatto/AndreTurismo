using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class AddressController
    {
        private AddressService _addressService;
        public AddressController()
        {
            _addressService = new AddressService();
        }
        public int Insert(Address address)
        {
            return _addressService.Insert(address);
        }

        public Address FindById(int id)
        {
            return _addressService.Find(id);
        }

        //public List<Address> FindByCity(City city, string search)
        //{
        //    return addressService.FindByCity(search);
        //}

        public List<Address> FindAll()
        {
             return _addressService.FindAll();
        }  
        
        public void UpdateAddress(Address address)
        {
            new CityService().UpdateById(address.City);
            _addressService.UpdateById(address);
        }       
        
        public void DeleteAddress(int id)
        {
            _addressService.Delete(id);
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
