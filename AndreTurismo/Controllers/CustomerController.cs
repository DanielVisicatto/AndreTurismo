using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class CustomerController
    {
        private CustomerService _customerService;
        public CustomerController()
        {
            _customerService = new CustomerService();
        }

        public int Create(Customer customer)
        {
            return _customerService.Create(customer);
        }
        public List<Customer> GetAll()
        {
            return _customerService.GetAll();
        }
        public Customer GetById(int id)
        {
            return _customerService.GetById(id);
        }
        public List<Customer> GetByName(string name)
        {
            return _customerService.GetByName(name);
        }
        public void Update(Customer customer)
        {
            _customerService.Update(customer);
        }
        public void Delete(int id)
        {
            _customerService.Delete(id);
        }
    }
}
