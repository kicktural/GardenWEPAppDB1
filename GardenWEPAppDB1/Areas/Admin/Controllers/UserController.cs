using GardenWEPAppDB1.Models;
using GardenWEPAppDB1.ViewModelVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]
    
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles1 = _userManager.Users.ToList();
            return View(roles1);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var role = (await _userManager.GetRolesAsync(user)).ToList();
            var roless = _roleManager.Roles.Select(X => X.Name).ToList();


            RoelModelVM ROELSmodelVM = new()
            {
                User = user,
                Roles = roless.Except(role)
            };
            return View(ROELSmodelVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string id, string role)
        {
            if (id == null) return NotFound();
            var user = (await _userManager.FindByIdAsync(id));
            if (user == null) return NotFound();

            var addrole = await _userManager.AddToRoleAsync(user, role);        

            if (!addrole.Succeeded)
            {
                ModelState.AddModelError("Error", "Role");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string Userid)
        {
            if (Userid == null) return NotFound();
            User user = await _userManager.FindByIdAsync(Userid);
            if (user == null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> Delete(string userid, string role)
        {
            if (userid == null || role == null) return NotFound();
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) return NotFound();

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role);

            if (!result.Succeeded)
            {
                ViewBag.Error = "Error Role!";
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
