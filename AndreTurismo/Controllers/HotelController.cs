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
        public int Insert(Hotel hotel)
        {
            return _hotelService.Insert(hotel);
        }
        public List<Hotel> FindAll()
        {
            return _hotelService.FindAll();
        }
        public Hotel FindById(int id)
        {
            return _hotelService.Find(id);
        }
        public List<Hotel> FindByName(string name)
        {
            return _hotelService.FindName(name);
        }
        public void UpdateHotel(Hotel hotel)
        {
            _hotelService.Update(hotel);
        }
        public void Delete(int id)
        {
            _hotelService.Delete(id);
        }
    }
}
