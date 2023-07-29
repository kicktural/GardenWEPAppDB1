using GardenWEPAppDB1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class DashBoardController : Controller
    {

        public IActionResult Index()
        {
          
            return View();
        }
    }
}
