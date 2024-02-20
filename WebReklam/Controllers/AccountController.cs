using ApplicationCore_WebReklam.DTO_s.AccountDTO;
using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebReklam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IMapper _mapper;




        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
           
        }

        //AllowAnonymous => Giriş yapmayan kişiler bu sayayı görüntüleyebilir!
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //ValidateAntiForgeryToken => siteye yapılan saldırıları engellemek ve kullanıcı bilgilerini korumak için kullanılır.
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser { LastName = model.LastName, UserName =model.Email, FirstName = model.FirstName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                IdentityResult result = await _userManager.CreateAsync(appUser);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Kayıt başarılı. Giriş yapabilirsiniz!";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Error"] = "Kayıt yapılamadı!";
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            TempData["Warning"] = "Lütfen kayıt oluşturma kurallarına uyunuz!";
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(model.Email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser.UserName, model.Password, false, false);

                    if (signInResult.Succeeded)
                    {              
                        if (await _userManager.IsInRoleAsync(appUser, "admin"))
                        {
                            TempData["Success"] = $"Hoşgeldin => Admin";
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }

                        TempData["Success"] = $"Hoşgeldin => {appUser.UserName}";
                        return RedirectToAction("Index", "Home", new { userName = appUser.UserName });
                    }
                }
            }
            TempData["Warning"] = "Kullanıcı adı veya şifre yanlış tekrar deneyin!";
            return View(model);
        }

        public async Task<IActionResult> Edit()
        {
            //User => Bu property giriş yapmış kullanıcının bilgilerine sahiptir. User.Identity.Name bize giriş yağpmış kullanıcının kullanıcı adını verir.
            //var appUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var userId = _userManager.GetUserId(HttpContext.User);
            var appUser = await _userManager.FindByIdAsync(userId);
            var model = new EditUserDTO(appUser);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var appUser = await _userManager.FindByIdAsync(userId);

                if (appUser != null)
                {
                    appUser.UserName = model.Email;
                    appUser.FirstName = model.FirstName;
                    appUser.LastName = model.LastName;
                    appUser.PhoneNumber = model.PhoneNumber;

                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
                    }

                    var result = await _userManager.UpdateAsync(appUser);

                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Profiliniz güncellendi! Tekrar giriş yapınız!";
                        // Giriş yapma işlemi gibi bir işlem yapılacaksa burada yönlendirme yapılabilir.
                        // Örneğin:
                        // return RedirectToAction("LogOut");
                    }
                    else
                    {
                        TempData["Error"] = "Profiliniz güncellenemedi!";
                    }
                }
                else
                {
                    TempData["Error"] = "Kullanıcı bulunamadı!";
                }
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Warning"] = "Başarılı bir şekilde çıkış yaptınız!";
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]

        public ViewResult PasswordChange()
        {
           return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDTO userPasswordChangeDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDTO.CurrentPassword);

                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDTO.CurrentPassword, userPasswordChangeDTO.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDTO.NewPassword, true, false);
                        TempData["Success"] = "Şifreniz Başarıyla güncellendi!";
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", errorMessage: "Girmiş olduğunuz şifre yanlıştır. Lütfen kontrol ederek tekrar deneyiniz.");
                        return View(userPasswordChangeDTO);
                    }

                }
                else
                {
                    ModelState.AddModelError("", errorMessage: "Girmiş olduğunuz şifre yanlıştır. Lütfen kontrol ederek tekrar deneyiniz.");

                    return View(userPasswordChangeDTO);
                }
            }
            else
            {
                return View(userPasswordChangeDTO);
            }
        }


    }
}
