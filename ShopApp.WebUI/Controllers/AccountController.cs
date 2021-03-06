using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.WebUI.Extensions;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    // kullanıcı da yanlıs veya token yoksa sayfalar gönderilmez...
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;
        public AccountController(ICartService cartService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            // if result.succeeded satırına breakpoint koyuyoruz daha sonra altsatıra gecerek callbackurl yi alıyoruz bu linkin basına localhosttaki url yi ekleyerek onaylamıs oluyoruz..
            if (result.Succeeded)
            {
                //generate token
                var Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail","Account",new {
                    userId = user.Id,
                    token = Token
                });
                // send email
                TempData.Put("message", new ResultMessage() 
                {
                    Title = "Hesap Onayı",
                    Message ="Eposta adresinize gelen link ile hesabınızı aktif edin",
                    Css = "warning"
                });
                return RedirectToAction("Login","Account");
            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz");

            return View(model);
        }

        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl

            }); ;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
           

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            //eğer username ile giris yapılacaksa....
            //  var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu email  ile hesap olusturulmamıs..");
                return View(model);
            }

            //eğer kullanıcı onaylanmamıs email ile giris yapmaya calısırsa diye bununda kontrolunu yapmamız lazım..
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Bu email onaylanmamıs,lütfen email i onaylayınız..");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false,false);
            if (result.Succeeded)
            {
                // return RedirectToAction("home","index");
               TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Girisi",
                    Message = "Kullanıcı basarılı sekilde giris yaptı",
                    Css = "success"
                });
                
                return Redirect(model.ReturnUrl ?? "~/");
                
            }
            ModelState.AddModelError("","email veya parola yanlış..");

            return View();
        }

       public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessage()
            {
                Title = "Oturum Kapatıldı.",
                Message = "Hesabınız güvenli bir şekilde sonlandırıldı.",
                Css = "warning"
            });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Hesap onayı için bilgiler yanlış",
                    Css = "warning"
                });
                return View(); 
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cartService.InitializeCart(user.Id);
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hesap Onayı",
                        Message = "Hesabınız başarılı bir şekilde onaylanmıştır...",
                        Css = "success"
                    });
                    return View();
                }

            }
            TempData["message"] = "Geçersiz kullanıcı.";
            return View();
        }

        public IActionResult ForgotPassword() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Sifre Sıfırlama",
                    Message = "Lütfen geçerli bir Email giriniz",
                    Css = "warning"
                });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {

                return View();
            }
            var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new 
            {
                userId = user.Id,
                token = Token
            });
            return RedirectToAction("Login","Account");
        }



        //eğer kullanıcı yetkisi olmadığı bir alana girerse
        public IActionResult Accessdenied()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult UserList() 
        {
            var users = _userManager.Users.ToList();
            return View(users);
       }

    }
}
