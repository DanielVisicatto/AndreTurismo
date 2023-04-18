using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class TicketController
    {
        public bool Insert (Ticket ticket)
        {
            return new TicketService().Insert (ticket);
        }

        public List<Ticket> FindAll()
        {
            return new TicketService().FindAll();
        }
    }
}
