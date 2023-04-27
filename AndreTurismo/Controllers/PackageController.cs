using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class PackageController
    {
        public int Create(Package package)
        {
            return new PackageService().Create(package);
        }

        public List<Package> GetAll()
        {
            return new PackageService().GetAll();
        }
    }
}
