using AndreTurismo.Controllers;
using AndreTurismo.Models;

Console.WriteLine("Proj - AndreTurismo");
Console.WriteLine("Incluindo dados...");

#region[Mocked Data]
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


Customer customer = new()
{
    Name = "Daniel",
    Address = address,
    PhoneNumber = "",
    CellPhoneNumber = "16 99751-9788",
    RegisterDate = DateTime.Now
};
customer.Id = new CustomerController().Insert(customer);

Hotel hotel = new()
{
    Name = "Real_Garden",
    Address = hotelAddress,
    RegisterDate = DateTime.Now,
    Price = 185.00
};
hotel.Id = new HotelController().Insert(hotel);

Ticket ticket = new()
{
    Home = customer.Address,
    Destiny = hotel.Address,
    Customer = customer,
    Date = DateTime.Now,
    Price = hotel.Price,
};
ticket.Id = new TicketController().Insert(ticket);

Package package = new()
{
    Hotel = hotel,
    Ticket = new() { Id = ticket.Id },
    RegisterDate = DateTime.Now,
    Price = 800.00 + hotel.Price,
    Customer = customer,
};
package.Id = new PackageController().Insert(package);

#endregion

Console.WriteLine("Selecione a opção desejada");





new PackageController().FindAll().ForEach(Console.WriteLine);

int Menu()
{

    Console.WriteLine("______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|              Proj - AndreTurismo            |");
    Console.WriteLine("|_____________________________________________|");
    Console.WriteLine("|***********    Dados Mockados    ************|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*          Selecione a opção desejada       *|");
    Console.WriteLine("|*___________________________________________*|");
    Console.WriteLine("|*   1  -  Clientes                          *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Endereços                         *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Hotéis                            *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Cidades                           *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Trazer todos os livros            *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Sair                              *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var opcao))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return opcao;
    }

}
