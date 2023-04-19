using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class PackageController
    {
        public int Insert(Package package)
        {
            return new PackageService().Insert(package);
        }

        public List<Package> FindAll()
        {
            return new PackageService().FindAll();
        }
    }
}
