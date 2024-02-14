using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebReklam.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Where(x => x.Status != ApplicationCore_WebReklam.Entities.Abstract.Status.Passive).ToListAsync();

            return View(users);

        }
    }
}
