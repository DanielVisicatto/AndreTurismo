using AndreTurismo.Controllers;
using AndreTurismo.Models;

int op;

#region[Mocked Data]
//City id = new()
//{
//    Description = "Araraquara",
//    RegisterDate = DateTime.Now,
//};
////city.Id = new CityController().Insert(city);

//City hotelCity = new()
//{
//    Description = "Sao Paulo",
//    RegisterDate = DateTime.Now,
//};

////hotelCity.Id = new CityController().Insert(hotelCity);

//Address address = new()
//{
//    Street = "Rua Dom Pedro I",
//    Number = 832,
//    Neighborhood = "Vila-Xavier",
//    City = id,
//    ZipCode = "14.810-108",
//    Complement = "FD",
//    RegisterDate = DateTime.Now
//};
////address.Id = new AddressController().Insert(address);

//Address hotelAddress = new()
//{
//    Street = "Av. Da Saudade",
//    Number = 1865,
//    Neighborhood = "Santa Clara",
//    City = hotelCity,
//    ZipCode = "11.252-850",
//    Complement = "",
//    RegisterDate = DateTime.Now,
//};
////hotelAddress.Id = new AddressController().Insert(hotelAddress);

//Customer customer = new()
//{
//    Name = "Daniel",
//    Address = address,
//    PhoneNumber = "",
//    CellPhoneNumber = "16 99751-9788",
//    RegisterDate = DateTime.Now
//};
////customer.Id = new CustomerController().Insert(customer);

//Hotel hotel = new()
//{
//    Name = "Real_Garden",
//    Address = hotelAddress,
//    RegisterDate = DateTime.Now,
//    Price = 185.00
//};
////hotel.Id = new HotelController().Insert(hotel);

//Ticket ticket = new()
//{
//    Home = customer.Address,
//    Destiny = hotel.Address,
//    Customer = customer,
//    Date = DateTime.Now,
//    Price = hotel.Price,
//};
////ticket.Id = new TicketController().Insert(ticket);

//Package package = new()
//{
//    Hotel = hotel,
//    Ticket = new() { Id = ticket.Id },
//    RegisterDate = DateTime.Now,
//    Price = 800.00 + hotel.Price,
//    Customer = customer,
//};
////package.Id = new PackageController().Insert(package);

#endregion

#region[Switch Option]
do
{
    Console.Clear();
    op = FirstMenu();
    switch (op)
    {
        default:
            Console.WriteLine("Opção Inválida");
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
                        customer.Id = new CustomerController().Insert(customer);
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
                        Address newAddress = new();
                        Console.WriteLine("Preencha com os dados do endereço");
                        Console.Write("Rua: ");
                        newAddress.Street = Console.ReadLine();
                        Console.Write("Nº: ");
                        if (!int.TryParse(Console.ReadLine(), out var numb))
                        {
                            Console.WriteLine("Número inválido");
                        }
                        else
                        {
                            newAddress.Number = numb;
                        }
                        Console.Write("Bairro: ");
                        newAddress.Neighborhood = Console.ReadLine();
                        Console.Write("CEP: ");
                        newAddress.ZipCode = Console.ReadLine();
                        Console.Write("Complemento: ");
                        newAddress.Complement = Console.ReadLine();
                        Console.WriteLine("CIDADES\n");
                        new CityController().FindAll().ForEach(Console.WriteLine);
                        Console.WriteLine("Digite ID da cidade");
                        if (!int.TryParse(Console.ReadLine(), out var cityId))
                        {
                            Console.WriteLine("ID inválido!");
                        }
                        else
                        {
                            City cityFound = new CityController().FindById(cityId);
                            newAddress.City = cityFound;
                        }

                        newAddress.RegisterDate = DateTime.Now;
                        newAddress.Id = new AddressController().Insert(newAddress);
                        Console.WriteLine("Registro atualizado com sucesso!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("ENDEREÇOS");
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        Console.WriteLine("Digite Id desejado");
                        
                        if (!int.TryParse(Console.ReadLine(), out var searchAddressId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {
                            Console.Clear();
                            Address addressFound = new AddressController().FindById(searchAddressId);
                           
                            if (addressFound != null)
                            { 
                                Console.WriteLine("Registro Selecionado:");
                                Console.WriteLine(addressFound);
                            }
                            else
                            {
                                Console.WriteLine("Registro não encontrado.");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("ENDEREÇOS\n");
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:                        
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        Console.Write("Digite o ID para editar: ");
                        if (!int.TryParse(Console.ReadLine(), out var addressId))
                        {
                            Console.WriteLine("Id inválido");
                        }
                        else
                        {
                            Address addressFound = new();
                            addressFound.Id = addressId;
                            Console.Write("Logradouro: ");
                            addressFound.Street = Console.ReadLine();
                            Console.Write("Nº: ");
                            addressFound.Number = int.Parse(Console.ReadLine());
                            Console.Write("Bairro: ");
                            addressFound.Neighborhood = Console.ReadLine();
                            Console.Write("CEP:");
                            addressFound.ZipCode = Console.ReadLine();
                            Console.Write("Complemento: ");

                            addressFound.Complement = Console.ReadLine();
                            new CityController().FindAll().ForEach(Console.WriteLine);
                            Console.Write("Escolha o ID da Cidade ");

                            if (!int.TryParse(Console.ReadLine(), out var cityFoundId))
                            {
                                Console.WriteLine("Cidade não encontrada.");
                            }
                            else
                            {
                                City cityFound = new CityController().FindById(cityFoundId);
                                addressFound.RegisterDate = DateTime.Now;
                                addressFound.City = cityFound;
                                new AddressController().UpdateAddress(addressFound);
                                Console.WriteLine("Registro atualizado!");
                                Console.WriteLine();
                                Console.WriteLine(addressFound);
                            }                   

                        }
                        Console.ReadLine();
                        break;

                    case 5:
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        Console.WriteLine("              ATENÇÃO!             \n" +
                                          "ESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
                        Console.Write("Digite o ID para DELETAR: ");
                        if(!int.TryParse(Console.ReadLine(), out var addressToDelete))
                        {
                            Console.WriteLine("Id inválido!");
                        }
                        {
                            try
                            {
                                new AddressController().DeleteAddress(addressToDelete);
                                Console.WriteLine("Endereço excluído da base de dados!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro ao deletar cidade. {e.Message}");
                            }
                        }

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
                        Console.WriteLine("Preencha com os dados do Hotel");
                        Hotel newHotel = new();
                        Console.Write("Nome: ");
                        newHotel.Name = Console.ReadLine();
                        Console.Write("Preço: ");
                        if(!float.TryParse(Console.ReadLine(), out var price))
                        {
                            Console.WriteLine("Preco inválido");
                        }
                        else
                        {
                             newHotel.Price = price;
                        }
                        new AddressController().FindAll().ForEach(Console.WriteLine);
                        newHotel.RegisterDate = DateTime.Now;
                        Console.Write("Selecione o Endereço: ");
                        if(!int.TryParse(Console.ReadLine(), out var idAddress))
                        {
                            Console.WriteLine("Id do endereço inválido!");
                        }
                        else
                        {
                            Address addressFound = new AddressController().FindById(idAddress);
                            newHotel.Address = addressFound;
                        }
                        newHotel.Id = new HotelController().Insert(newHotel);
                        Console.WriteLine("Registro atualizado com sucesso!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Digite o nome do Hotel para buscar");
                        string? searchHotel = Console.ReadLine();

                        if (searchHotel != null)
                        {
                            Console.WriteLine("HOTEL\n");
                            HotelController hc = new();
                            List<Hotel> hotelFound = hc.FindByName(searchHotel);
                            if (hotelFound != null)
                            {
                                hotelFound.ForEach(x => Console.WriteLine(x));
                            }
                            else
                            {
                                Console.WriteLine("Cidade não encontrada");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Oportunidade não disponível!");
                        }
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
                        Console.WriteLine("Insira Descrição da cidade:");
                        string newcityDesc = Console.ReadLine();

                        City newCity = new()
                        {
                            Description = newcityDesc,
                            RegisterDate = DateTime.Now
                        };
                        newCity.Id = new CityController().Insert(newCity);
                        Console.WriteLine("Registro incluido com sucesso!");
                        Console.WriteLine(newCity);

                        Console.ReadLine();
                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("Nome da cidade: ");
                        string? searchDesc = Console.ReadLine();

                        if (searchDesc != null)
                        {
                            Console.WriteLine("CIDADE\n");
                            CityController cc = new();
                            List<City> cityFound = cc.FindByDescription(searchDesc);
                            if (cityFound != null)
                            {
                                cityFound.ForEach(x => Console.WriteLine(x));
                            }
                            else
                            {
                                Console.WriteLine("Cidade não encontrada");
                            }

                        }
                        else { Console.WriteLine("Oportunidade não disponível!"); }
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("CIDADES\n");
                        new CityController().FindAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        new CityController().FindAll().ForEach(Console.WriteLine);
                        Console.Write("Digite o ID para editar: ");
                        if (!int.TryParse(Console.ReadLine(), out var cityId))
                        {
                            Console.WriteLine("Id inválido");
                        }
                        else
                        {
                            City cityFound = new();
                            Console.Clear();
                            Console.WriteLine("Nova Descrição:");
                            cityFound.Description = Console.ReadLine();
                            cityFound.RegisterDate = DateTime.Now;
                            cityFound.Id = cityId;
                            new CityController().UpdateCity(cityId, cityFound);
                            Console.Clear();
                            Console.WriteLine("Registro atualizado!");
                            Console.WriteLine();
                            Console.WriteLine(cityFound);
                        }
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.Clear();
                        new CityController().FindAll().ForEach(Console.WriteLine);
                        Console.WriteLine("              ATENÇÃO!             \n" +
                                          "ESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
                        Console.Write("Digite o ID para DELETAR: ");
                        City cityToDelete = new();
                        if (!int.TryParse(Console.ReadLine(), out var idToDelete))
                        {
                            Console.WriteLine("Id inválido");
                        }
                        else
                        {
                            try
                            {
                                new CityController().DeleteCity(idToDelete);
                                Console.WriteLine("Cidade excluída da base de dados!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro ao deletar cidade. {e.Message}");
                            }
                        }
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
                        ticket.Id = new TicketController().Insert(ticket);
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
                op6 = PackageMenu();
                switch (op6)
                {
                    default:
                        Console.WriteLine("Opção inválida");
                        op6 = 4;
                        break;

                    case 1:
                        package.Id = new PackageController().Insert(package);
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