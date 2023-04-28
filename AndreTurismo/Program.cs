using AndreTurismo.Controllers;
using AndreTurismo.Models;

int op;

#region[Mocked Data]
City id = new()
{
    Description = "Araraquara",
    RegisterDate = DateTime.Now,
};
//city.Id = new CityController().Insert(city);

City hotelCity = new()
{
    Description = "Sao Paulo",
    RegisterDate = DateTime.Now,
};

//hotelCity.Id = new CityController().Insert(hotelCity);

Address address = new()
{
    Street = "Rua Dom Pedro I",
    Number = 832,
    Neighborhood = "Vila-Xavier",
    City = id,
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
                        Customer newCustomer = new();
                        Console.WriteLine("Preencha os dados do Cliente.");
                        Console.Write("None: ");
                        newCustomer.Name = Console.ReadLine();
                        Console.Write("Telefone: ");
                        newCustomer.PhoneNumber = Console.ReadLine();
                        Console.Write("Celular: ");
                        newCustomer.CellPhoneNumber = Console.ReadLine();
                        Console.Clear();
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        Console.Write("Selecione o Endereço: ");
                        if (!int.TryParse(Console.ReadLine(), out var idAddress))                        
                            Console.WriteLine("Id do endereço inválido!");                        
                        else
                        {
                            Address addressFound = new AddressController().GetById(idAddress);
                            newCustomer.Address = addressFound;
                        }
                        newCustomer.RegisterDate = DateTime.Now;
                        newCustomer.Id = new CustomerController().Create(newCustomer);
                        Console.Clear();
                        Console.WriteLine("Cliente cadastrado com sucesso!");

                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("Digite o nome do(a) cliente para buscar");
                        var customerSearch = Console.ReadLine();
                        Console.Clear();
                        try
                        {
                            new CustomerController().GetByName(customerSearch).ForEach(Console.WriteLine);
                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine("Cliente não encontrado(a)");
                            throw new(ex.Message);
                        }
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.WriteLine("CLIENTES\n");
                        new CustomerController().GetAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        new CustomerController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine();
                        Console.WriteLine("Digite o ID do Cliente para edita os dados.");
                        Customer customerById = new();
                        if (!int.TryParse(Console.ReadLine(), out var idCustomerSearch))
                            Console.WriteLine("Numero de ID inválido!");
                        else
                        {
                            Console.Clear();
                            customerById = new CustomerController().GetById(idCustomerSearch);

                            if (customerById == null) break;

                            Console.WriteLine($"Entre com os novos dados do(a) Cliente {customerById.Name}");
                            Console.Write("Nome: ");
                            customerById.Name = Console.ReadLine();
                            Console.Write("Telefone: ");
                            customerById.PhoneNumber = Console.ReadLine();
                            Console.Write("Celular: ");
                            customerById.CellPhoneNumber = Console.ReadLine();
                            Console.WriteLine("Deseja alterar o endereço do Cliente? (S/N)");
                            var answ = Console.ReadLine();
                            if (answ == "s")
                            {
                                new AddressController().GetAll().ForEach(Console.WriteLine);

                                Console.WriteLine("Escolha o ID do endereço");
                                if (!int.TryParse(Console.ReadLine(), out var newHotelAddress))
                                {
                                    Console.WriteLine("Numero de Id inválido!");
                                    break;
                                }
                                else
                                    customerById.Address = new AddressController().GetById(newHotelAddress);
                            }
                            customerById.RegisterDate = DateTime.Now;
                            new CustomerController().Update(customerById);
                            Console.WriteLine("Registro alterado com sucesso.");
                            Console.ReadLine();
                        }
                        break;

                    case 5:
                        new CustomerController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("              ATENÇÃO!             \n" +
                                          "ESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
                        Console.Write("Digite o ID para DELETAR: ");
                        if (!int.TryParse(Console.ReadLine(), out var customerToDelete))
                            Console.WriteLine("Id inválido!");
                        else
                        {
                            try
                            {
                                new CustomerController().Delete(customerToDelete);
                                Console.WriteLine("Cliente excluído(a) da base de dados!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro ao deletar Cliente. {e.Message}");
                            }
                        }
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
                        new CityController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("Digite ID da cidade");
                        if (!int.TryParse(Console.ReadLine(), out var cityId))
                        {
                            Console.WriteLine("ID inválido!");
                        }
                        else
                        {
                            City cityFound = new CityController().GetById(cityId);
                            newAddress.City = cityFound;
                        }

                        newAddress.RegisterDate = DateTime.Now;
                        newAddress.Id = new AddressController().Create(newAddress);
                        Console.WriteLine("Registro atualizado com sucesso!");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("ENDEREÇOS");
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("Digite Id desejado");
                        
                        if (!int.TryParse(Console.ReadLine(), out var searchAddressId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {
                            Console.Clear();
                            Address addressFound = new AddressController().GetById(searchAddressId);
                           
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
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:                        
                        new AddressController().GetAll().ForEach(Console.WriteLine);
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
                            new CityController().GetAll().ForEach(Console.WriteLine);
                            Console.Write("Escolha o ID da Cidade ");

                            if (!int.TryParse(Console.ReadLine(), out var cityFoundId))
                            {
                                Console.WriteLine("Cidade não encontrada.");
                            }
                            else
                            {
                                City cityFound = new CityController().GetById(cityFoundId);
                                addressFound.RegisterDate = DateTime.Now;
                                addressFound.City = cityFound;
                                new AddressController().Update(addressFound);
                                Console.WriteLine("Registro atualizado!");
                                Console.WriteLine();
                                Console.WriteLine(addressFound);
                            }                   

                        }
                        Console.ReadLine();
                        break;

                    case 5:
                        new AddressController().GetAll().ForEach(Console.WriteLine);
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
                                new AddressController().Delete(addressToDelete);
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
                            Console.WriteLine("Preco inválido");                        
                        else
                        {
                             newHotel.Price = price;
                        }
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        newHotel.RegisterDate = DateTime.Now;
                        Console.Write("Selecione o Endereço: ");
                        if(!int.TryParse(Console.ReadLine(), out var idAddress))                        
                            Console.WriteLine("Id do endereço inválido!");                        
                        else
                        {
                            Address addressFound = new AddressController().GetById(idAddress);
                            newHotel.Address = addressFound;
                        }
                        newHotel.Id = new HotelController().Create(newHotel);
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
                            List<Hotel> hotelFound = hc.GetByName(searchHotel);
                            if (hotelFound != null)                            
                                hotelFound.ForEach(x => Console.WriteLine(x));                            
                            else                            
                                Console.WriteLine("Hotel não encontrado");                            
                        }
                        else                        
                            Console.WriteLine("Oportunidade não disponível!");
                        
                        Console.ReadLine();                        
                        break;

                    case 3:
                        Console.WriteLine("HOTÉIS\n");
                        new HotelController().GetAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        new HotelController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine();
                        Console.WriteLine("Digite o ID do hotel para edita os dados.");
                        Hotel hotelById = new();
                        if (!int.TryParse(Console.ReadLine(), out var idHotelSearch))                        
                            Console.WriteLine("Numero de ID inválido!");                        
                        else
                        {
                            //Console.Clear();
                            hotelById = new HotelController().GetById(idHotelSearch);

                            if (hotelById == null) break;

                            Console.WriteLine($"Entre com os novos dados do Hotel {hotelById.Name}");
                            Console.Write("Nome: ");
                            hotelById.Name = Console.ReadLine();
                            Console.Write("Price: ");
                            hotelById.Price = float.Parse(Console.ReadLine());

                            Console.WriteLine("Deseja alterar o endereço do Hotel? (S/N)");
                            var answ = Console.ReadLine();
                            if(answ == "s")
                            {
                                new AddressController().GetAll().ForEach(Console.WriteLine);

                                Console.WriteLine("Escolha o ID do endereço");
                                if (!int.TryParse(Console.ReadLine(), out var newHotelAddress))
                                {
                                    Console.WriteLine("Numero de Id inválido!");
                                    break;
                                }
                                else                                
                                    hotelById.Address = new AddressController().GetById(newHotelAddress);                                
                            }                    

                            hotelById.RegisterDate = DateTime.Now;
                            new HotelController().Update(hotelById);
                            Console.WriteLine("Registro alterado com sucesso.");
                        }
                        
                        Console.ReadLine();
                        break;

                    case 5:
                        new HotelController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("              ATENÇÃO!             \n" +
                                          "ESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
                        Console.Write("Digite o ID para DELETAR: ");
                        if (!int.TryParse(Console.ReadLine(), out var hotelToDelete))                        
                            Console.WriteLine("Id inválido!");                        
                        else
                        {
                            try
                            {
                                new HotelController().Delete(hotelToDelete);
                                Console.WriteLine("Hotel excluído da base de dados!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro ao deletar hotel. {e.Message}");
                            }
                        }
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
                        newCity.Id = new CityController().Create(newCity);
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
                            List<City> cityFound = cc.GetByDesc(searchDesc);
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
                        new CityController().GetAll().ForEach(Console.WriteLine);
                        Console.ReadLine();
                        break;

                    case 4:
                        new CityController().GetAll().ForEach(Console.WriteLine);
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
                            new CityController().Update(cityId, cityFound);
                            Console.Clear();
                            Console.WriteLine("Registro atualizado!");
                            Console.WriteLine();
                            Console.WriteLine(cityFound);
                        }
                        Console.ReadLine();
                        break;

                    case 5:
                        Console.Clear();
                        new CityController().GetAll().ForEach(Console.WriteLine);
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
                                new CityController().Delete(idToDelete);
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
                        op5 = 5;
                        break;
                    case 1:
                        Ticket newTicket = new();
                        Console.WriteLine("Preencha com os dados da Passagem");
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        Console.Write("Endereço de Origem: ");
                        if(!int.TryParse(Console.ReadLine(), out var ticketHomeId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {
                            newTicket.Home = new AddressController().GetById(ticketHomeId);
                            Console.Clear();
                        }
                        new AddressController().GetAll().ForEach(Console.WriteLine);
                        Console.Write("Endereço de Destino: ");
                        if (!int.TryParse(Console.ReadLine(), out var ticketDestinyId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {
                            newTicket.Destiny = new AddressController().GetById(ticketDestinyId);
                            Console.Clear();
                        }
                        new CustomerController().GetAll().ForEach(Console.WriteLine);                        
                        Console.Write("Selecione Cliente: ");
                        if (!int.TryParse(Console.ReadLine(), out var ticketCustomerId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {                            
                            newTicket.Customer = new CustomerController().GetById(ticketCustomerId);
                            Console.Clear();
                        }
                        Console.Write("Digite o ID do cliente: ");
                        newTicket.Date = DateTime.Now;
                        Console.Write("Valor do pacote: ");
                        if(!float.TryParse(Console.ReadLine(), out var TicketPrice))
                        {
                            Console.WriteLine("valor inexistente.");
                        }
                        else
                        {
                            newTicket.Price = TicketPrice;
                        }
                        
                        break;

                    case 2:
                        Console.WriteLine("PASSAGENS");
                        new TicketController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("Digite Id desejado");

                        if (!int.TryParse(Console.ReadLine(), out var searchTicketId))
                        {
                            Console.WriteLine("ID digitado inválido!");
                        }
                        else
                        {
                            Console.Clear();
                            Ticket ticketFound = new TicketController().GetById(searchTicketId);

                            if (ticketFound != null)
                            {
                                Console.WriteLine("Registro Selecionado:");
                                Console.WriteLine(ticketFound);
                            }
                            else
                            {
                                Console.WriteLine("Registro não encontrado.");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("PASSAGENS");
                        new TicketController().GetAll().ForEach(Console.WriteLine);                        
                       
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.Clear();
                        new TicketController().GetAll().ForEach(Console.WriteLine);
                        Console.WriteLine("              ATENÇÃO!             \n" +
                                          "ESTA OPERAÇÃO NÃO PODE SER DESFEITA!");
                        Console.Write("Digite o ID para DELETAR: ");
                        if (!int.TryParse(Console.ReadLine(), out var ticketToDelete))
                            Console.WriteLine("Id inválido!");
                        else
                        {
                            try
                            {
                                new TicketController().Delete(ticketToDelete);
                                Console.WriteLine("Passagem excluída da base de dados!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Erro ao deletar Pasagem. {e.Message}");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case 5:
                        break;
                }
            } while (op5 != 5);
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
                        package.Id = new PackageController().Create(package);
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.WriteLine("PACOTES\n");
                        new PackageController().GetAll().ForEach(Console.WriteLine);
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
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   1  -  Cadastrar Passagem                *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   2  -  Buscar Passagem                   *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   3  -  Buscar Todas as Passagens         *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   4  -  Deletar Passagem                  *|");
    Console.WriteLine("|*                                           *|");
    Console.WriteLine("|*   5  -  Voltar                            *|");
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