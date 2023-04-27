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
        public int Create(Address address)
        {
            return _addressService.Create(address);
        }

        public Address GetById(int id)
        {
            return _addressService.GetById(id);
        }

        //public List<Address> FindByCity(City city, string search)
        //{
        //    return addressService.FindByCity(search);
        //}

        public List<Address> GetAll()
        {
             return _addressService.GetAll();
        }  
        
        public void Update(Address address)
        {
            new CityService().Update(address.City);
            _addressService.Update(address);
        }       
        
        public void Delete(int id)
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
