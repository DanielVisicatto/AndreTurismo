using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class PackageController
    {
        public bool Insert(Package package)
        {
            return new PackageService().Insert(package);
        }

        public List<Hotel> FindAll()
        {
            return new PackageService().FindAll();
        }
    }
}
