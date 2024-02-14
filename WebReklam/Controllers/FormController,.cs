using ApplicationCore_WebReklam.DTO_s.RequestFormDTO;
using ApplicationCore_WebReklam.Entities.Concrete;
using AutoMapper;
using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebReklam.Controllers
{
    public class FormController : Controller
    {
        private readonly IRequestFormRepository _requestFormRepository;
        private readonly IMapper _mapper;

        public FormController(IRequestFormRepository requestFormRepository, IMapper mapper)
        {
            _requestFormRepository = requestFormRepository;
            _mapper = mapper;
        }



        public IActionResult GetForm() => View();

        [HttpPost]
        public async Task<IActionResult> GetForm(CreateRequestFormDTO model)
        {
            if (ModelState.IsValid)
            {
                var request = _mapper.Map<RequestForm>(model);
                await _requestFormRepository.AddAsync(request);
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
