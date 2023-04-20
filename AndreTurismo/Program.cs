using AndreTurismo.Controllers;
using AndreTurismo.Models;
int op;


#region[Mocked Data]
City city = new()
{
    Description = "Araraquara",
    RegisterDate = DateTime.Now,
};
//city.Id = new CityController().Insert(city);

City hotelCity = new City()
{
    Description = "São Paulo",
    RegisterDate = DateTime.Now,
};

//hotelCity.Id = new CityController().Insert(hotelCity);

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
//address.Id = new AddressController().Insert(address);

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
//hotelAddress.Id = new AddressController().Insert(hotelAddress);

Customer customer = new()
{
    Name = "Daniel",
    Address = address,
    PhoneNumber = "",
    CellPhoneNumber = "16 99751-9788",
    RegisterDate = DateTime.Now
};
//customer.Id = new CustomerController().Insert(customer);

Hotel hotel = new()
{
    Name = "Real_Garden",
    Address = hotelAddress,
    RegisterDate = DateTime.Now,
    Price = 185.00
};
//hotel.Id = new HotelController().Insert(hotel);

Ticket ticket = new()
{
    Home = customer.Address,
    Destiny = hotel.Address,
    Customer = customer,
    Date = DateTime.Now,
    Price = hotel.Price,
};
//ticket.Id = new TicketController().Insert(ticket);

Package package = new()
{
    Hotel = hotel,
    Ticket = new() { Id = ticket.Id },
    RegisterDate = DateTime.Now,
    Price = 800.00 + hotel.Price,
    Customer = customer,
};
//package.Id = new PackageController().Insert(package);

#endregion



#region[Switch Option]
do
{
    Console.Clear();
    op = FirstMenu();
    switch (op)
    {
        default: Console.WriteLine("Opção Inválida");            
            break;
        case 1:
            int op1;
            do
            {                 
                Console.Clear();
                op1 = CustomerMenu();
                switch (op1)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op1 = 6;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;
                        
                    case 2:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("CLIENTES\n");
                        new CustomerController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 6:
                        break;
                }
            } while (op1 != 6);            
            break;

        case 2:            
            Console.Clear();
            int op2;
            do
            {
                Console.Clear();
                op2 = AddressMenu();
                switch (op2)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op2 = 6;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("ENDEREÇOS\n");
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 6:
                        break;
                }
            } while (op2 != 6);
            break;

        case 3:
            Console.Clear();            
            int op3;
            do
            {
                Console.Clear();
                op3 = HotelMenu();
                switch (op3)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op3 = 6;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("HOTÉIS\n");
                        new HotelController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 6:
                        break;
                }
            } while (op3 != 6);            
            break;

        case 4:
            Console.Clear();
            int op4;
            do
            {
                Console.Clear();
                op4 = CityMenu();
                switch (op4)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op4 = 6;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("CIDADES\n");
                        new CityController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 6:
                        break;
                }
            } while (op4 != 6);
            break;

        case 5:
            Console.Clear();
            int op5;
            do
            {
                Console.Clear();
                op5 = TicketMenu();
                switch (op5)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op5 = 4;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("PASSAGENS\n");
                        new TicketController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;                    

                    case 4:
                        break;
                }
            } while (op5 != 4);
            break;

        case 6:
            Console.Clear();
            int op6;
            do
            {
                Console.Clear();
                op6 = TicketMenu();
                switch (op6)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op6 = 4;
                        break;

                    case 1:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("PACOTES\n");
                        new PackageController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("Esta oportunidade ficará disponível em breve!");
                        Console.ReadLine();
                        break;

                    case 4:
                        break;
                }
            } while (op6 != 4);
            break;

        case 7:
            Console.Clear();
            Console.WriteLine("Obrigado por utilizar nossos serviços!");
            Environment.Exit(0);
            break;
    }        
} while (op != 7);
#endregion

#region[InteractiveMenu]
int FirstMenu()
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
    Console.WriteLine("|*   5  -  Passagem                          *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Pacotes                           *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   7  -  Sair                              *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var option))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return option;
    }

}
int CustomerMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Cadastrar Cliente                 *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Cliente                    *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Listar Clientes                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Atualizar Cliente                 *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Deletar                           *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var customerOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return customerOption;
    }
}
int AddressMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Cadastrar Endereço                *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Endereço                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Listar Endereços                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Atualizar Endereço                *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Deletar Endereço                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var addressOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return addressOption;
    }
}
int HotelMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Cadastrar Hotel                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Hotel                      *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Listar Hotéis                     *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Atualizar Hotel                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Deletar hotel                     *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var hotelOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return hotelOption;
    }
}
int CityMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Cadastrar Cidade                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Cidade                     *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Listar Cidades                    *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Atualizar Cidade                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Deletar Cidade                    *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   6  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var cityOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return cityOption;
    }
}
int TicketMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Buscar Passagem                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Todas as Passagens         *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Deletar Passagem                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var ticketOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return ticketOption;
    }
}
int PackageMenu()
{
    Console.WriteLine("_______________________________________________");
    Console.WriteLine("|                                             |");
    Console.WriteLine("|*   1  -  Buscar Pacote                     *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Todos os Pacotes           *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Deletar Pacote                    *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Voltar                            *|");
    Console.WriteLine("|*___________________________________________*|");

    if (!int.TryParse(Console.ReadLine(), out var packageOption))
    {
        Console.Clear();
        Console.WriteLine("Opção inválida");
        Thread.Sleep(2000);
        Console.Clear();

        return 0;
    }
    else
    {
        return packageOption;
    }
}
#endregion