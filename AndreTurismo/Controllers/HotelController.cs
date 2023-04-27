using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class HotelController
    {
        private HotelService _hotelService;
        public HotelController()
        {
             _hotelService = new HotelService();
        }
        public int Create(Hotel hotel)
        {
            return _hotelService.Create(hotel);
        }
        public List<Hotel> GetAll()
        {
            return _hotelService.GetAll();
        }
        public Hotel GetById(int id)
        {
            return _hotelService.GetById(id);
        }
        public List<Hotel> GetByName(string name)
        {
            return _hotelService.GetByName(name);
        }
        public void Update(Hotel hotel)
        {
            _hotelService.Update(hotel);
        }
        public void Delete(int id)
        {
            _hotelService.Delete(id);
        }
    }
}
