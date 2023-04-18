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
//new CityController().FindAll().ForEach(Console.WriteLine);
Console.WriteLine(city.Id);

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
new AddressController().FindAll().ForEach(Console.WriteLine);

Customer customer = new()
{
    Name = "Daniel",
    Address = address,
    PhoneNumber = "",
    CellPhoneNumber = "16 99751-9788",
    RegisterDate = DateTime.Now
};
//new CustomerController().Insert(customer);
//new CustomerController().FindAll().ForEach(Console.WriteLine);

//Hotel hotel = new()
//{
//    Id = 1,
//    Name = "Real_Garden",
//    Address = hotelAddress,
//    RegisterDate = DateTime.Now,
//    Price = 185.00
//};

//Ticket ticket = new()
//{
//    Id = 1,
//    Home = customer.Address,
//    Destiny = hotel.Address,
//    Customer = customer,
//    Date = DateTime.Now,
//    Price = hotel.Price,
//};

//Package package = new()
//{
//    Id = 1,
//    Hotel = hotel,
//    Ticket = ticket,
//    RegisterDate = DateTime.Now,
//    Price = 800.00 + hotel.Price,
//    Customer = customer,
//};