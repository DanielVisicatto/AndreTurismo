using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class CustomerController
    {
        public bool Insert(Customer customer)
        {
            return new CustomerService().Insert(customer);
        }

        public List<Customer> FindAll()
        {
            return new CustomerService().FindAll();
        }
    }
}
