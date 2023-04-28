using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class TicketController
    {

        private TicketService _ticketService;
        public TicketController()
        {
            _ticketService = new TicketService();
        }

        public int Create (Ticket ticket)
        {
            return _ticketService.Create(ticket);
        }
        public List<Ticket> GetAll()
        {
            return _ticketService.GetAll();
        }
        public Ticket GetById(int id)
        {
            return _ticketService.GetById(id);
        }
        public void Delete(int id)
        {
            _ticketService.Delete(id);
        }
    }
}
