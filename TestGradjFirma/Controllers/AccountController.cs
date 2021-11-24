using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestGradjFirma.Data;

namespace TestGradjFirma.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<IActionResult> Login(string username, string password)
        {

            try
            {
                var user = await _userManager.FindByNameAsync(username);


                if (user != null)
                {

                    var signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);
                    Debug.WriteLine(signInResult.ToString());

                    if (signInResult.Succeeded)
                    {

                        return RedirectToAction("Index", "Account");
                    }
                }
                return RedirectToAction("Login");
            }
            catch (ArgumentNullException e)
            {
                return Content("oS KURCA");
            }
            catch (Exception e)
            {
                return Content("Ovo je message:\t" + e.Message + "\n A ovo je string:\n" + e.ToString());

            }
        }
        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = "",

            };

            var result = await _userManager.CreateAsync(user, password);

            Debug.WriteLine(result);
            if (result.Succeeded)
            {

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); ;
        }

        public IActionResult Register()
        {

            return View();
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
