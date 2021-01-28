using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using animated_spoon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace animated_spoon.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private IProductRepository productRepository;


        private const string adminUser = "admin";
        private const string adminPassword = "Test123!";
        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.EnsurePopulated();

        }

        public async void EnsurePopulated()
        {
            IdentityUser user = await this.userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("admin");
                await this.userManager.CreateAsync(user, adminPassword);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user =
                    await userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user,
                            loginModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect("/Admin/Products");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


    }
}
