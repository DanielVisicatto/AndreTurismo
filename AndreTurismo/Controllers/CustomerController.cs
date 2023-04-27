using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class CustomerController
    {
        public int Create(Customer customer)
        {
            return new CustomerService().Insert(customer);
        }

        public List<Customer> GetAll()
        {
            return new CustomerService().GetAll();
        }
    }
}
