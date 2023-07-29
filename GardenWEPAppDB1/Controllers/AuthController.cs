using GardenWEPAppDB1.DTOs;
using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace GardenWEPAppDB1.Controllers
{

    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Error", "Email confirm");
            }


            var CheckEmail1 = await _userManager.FindByEmailAsync(loginDto.Email);

            if (CheckEmail1 == null)
            {
                ModelState.AddModelError("Error", "Email Page!");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result1 = await _signInManager.PasswordSignInAsync(CheckEmail1, loginDto.Password, loginDto.RememBerme, false);

            if (!result1.Succeeded)
            {
                ModelState.AddModelError("error", "Password!");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var checEmail = await _userManager.FindByEmailAsync(registerDto.Email);

            if (checEmail != null)
            {
                ModelState.AddModelError("Error", "Email");
                
            }

            User user = new()
            {
                FistName = registerDto.FisrtName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                PhotoUrl = "/",
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Error", item.Description);
                }
                return View(registerDto);
            }

        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
