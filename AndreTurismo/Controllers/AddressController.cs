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

        public Address FindById(int id)
        {
            return addressService.Find(id);
        }

        //public List<Address> FindByCity(City city, string search)
        //{
        //    return addressService.FindByCity(search);
        //}

        public List<Address> FindAll()
        {
             return addressService.FindAll();
        }  
        
        public void UpdateAddress(Address address)
        {
            new CityService().UpdateById(address.City);
            addressService.UpdateById(address);
        }       
        
        //public void DeleteAddress(int id, City city)
        //{
        //    addressService.Delete(id, city);
        //}




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
