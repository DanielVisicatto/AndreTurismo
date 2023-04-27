using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class TicketController
    {
        public int Create (Ticket ticket)
        {
            return new TicketService().Create(ticket);
        }

        public List<Ticket> GetAll()
        {
            return new TicketService().GetAll();
        }
    }
}
