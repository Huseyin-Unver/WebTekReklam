using ApplicationCore_WebReklam.DTO_s.AccountDTO;
using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Web;
using WebReklam.Model;

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
                   
                    appUser.FirstName = model.FirstName;
                    appUser.LastName = model.LastName;
                    appUser.PhoneNumber = model.PhoneNumber;
                    await _userManager.UpdateSecurityStampAsync(appUser);
                    return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
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
        public IActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(user.Email);
                mail.From = new MailAddress("unverler123@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre Güncelleme Talebi";
                mail.Body = $"<a target=\"_blank\" href=\"https://localhost:7285{Url.Action("UpdatePassword", "Account", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
                mail.IsBodyHtml = true;
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("unverler123@gmail.com", "lkljreformremoje");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                smp.Send(mail);

                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View();
        }

        [HttpGet("[action]/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }
        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
                ViewBag.State = false;
            return View();
        }
    }
}

