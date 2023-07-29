using GardenWEPAppDB1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin, Super Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;        
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole IdentityRole)
        {

            if (IdentityRole == null) return View();

            var Checkrole = await _roleManager.FindByNameAsync(IdentityRole.Name);

            if (Checkrole != null)
            {
                ViewBag.Errors = "The role is exsist!!";
                return View();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Role name is exsis!!!");
                return View();
            }
            else
            {
                await _roleManager.CreateAsync(IdentityRole);
                return RedirectToAction("Index");
            }


        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(IdentityRole role)
        {

            _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Detail(string name)
        {
            if (name == null) return NotFound();
            var tags = _roleManager.Roles.FirstOrDefault(x => x.Name == name);
            if (tags == null) return NotFound();
            return View(tags);
        }

    }
}
