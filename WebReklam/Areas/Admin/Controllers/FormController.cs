using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebReklam.Areas.Admin.Models;

namespace WebReklam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormController : Controller
    {
        private readonly IRequestFormRepository _requestFormRepository;

        public FormController(IRequestFormRepository requestFormRepository)
        {
            _requestFormRepository = requestFormRepository;
        }

        public async Task<IActionResult> Index()
        {
            var form = await _requestFormRepository.GetFilteredList
                (
                    select: x => new FormVM
                    {

                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        CompanyAddress = x.CompanyAddress,
                        CompanyName = x.CompanyName,
                        Email = x.Email,
                        Image = x.Image,
                        
                        PhoneNumber = x.PhoneNumber,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != ApplicationCore_WebReklam.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(form);
        }

    }
}
