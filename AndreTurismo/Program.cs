using AndreTurismo.Controllers;
using AndreTurismo.Models;


Console.WriteLine("Proj - AndreTurismo");
Console.WriteLine("Incluindo dados");

City city = new()
{
    Description = "Araraquara",
    RegisterDate = DateTime.Now,
};
city.Id = new CityController().Insert(city);


City hotelCity = new City()
{
    Description = "São Paulo",
    RegisterDate = DateTime.Now,
};
hotelCity.Id = new CityController().Insert(hotelCity);
//new CityController().FindAll().ForEach(Console.WriteLine);

Address address = new()
{
    Street = "Rua Dom Pedro I",
    Number = 832,
    Neighborhood = "Vila-Xavier",
    City = city,
    ZipCode = "14.810-108",
    Complement = "FD",
    RegisterDate = DateTime.Now
};
address.Id = new AddressController().Insert(address);

Address hotelAddress = new()
{
    Street = "Av. Da Saudade",
    Number = 1865,
    Neighborhood = "Santa Clara",
    City = hotelCity,
    ZipCode = "11.252-850",
    Complement = "",
    RegisterDate = DateTime.Now,

};
hotelAddress.Id = new AddressController().Insert(hotelAddress);
//new AddressController().FindAll().ForEach(Console.WriteLine);

Customer customer = new()
{
    Name = "Daniel",
    Address = address,
    PhoneNumber = "",
    CellPhoneNumber = "16 99751-9788",
    RegisterDate = DateTime.Now
};
customer.Id = new CustomerController().Insert(customer);
//new CustomerController().FindAll().ForEach(Console.WriteLine);



Hotel hotel = new()
{    
    Name = "Real_Garden",
    Address = hotelAddress,
    RegisterDate = DateTime.Now,
    Price = 185.00
};
hotel.Id = new HotelController().Insert(hotel);
//new HotelController().FindAll().ForEach(Console.WriteLine);

Ticket ticket = new()
{
    Home = customer.Address,
    Destiny = hotel.Address,
    Customer = customer,
    Date = DateTime.Now,
    Price = hotel.Price,
};
ticket.Id = new TicketController().Insert(ticket);
//new TicketController().FindAll().ForEach(Console.WriteLine);

Package package = new()
{
    Hotel = hotel,
    Ticket = new() { Id = ticket.Id },
    RegisterDate = DateTime.Now,
    Price = 800.00 + hotel.Price,
    Customer = customer,
};
package.Id = new PackageController().Insert(package);
new PackageController().FindAll().ForEach(Console.WriteLine);