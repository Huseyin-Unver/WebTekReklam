using ApplicationCore_WebReklam.DTO_s.RequestFormDTO;
using ApplicationCore_WebReklam.Entities.Concrete;
using AutoMapper;
using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore_WebReklam.Entities.Abstract;


namespace WebReklam.Controllers
{
    public class FormController : Controller
    {
        private readonly IRequestFormRepository _requestFormRepository;
        private readonly IMapper _mapper;
        private readonly IVillageRepository _villageRepository;
        private readonly ICityRepository _cityRepository;

        public FormController(IRequestFormRepository requestFormRepository, IMapper mapper, IVillageRepository villageRepository, ICityRepository cityRepository)
        {
            _requestFormRepository = requestFormRepository;
            _mapper = mapper;
            _villageRepository = villageRepository;
            _cityRepository = cityRepository;
        }



        public async Task<IActionResult> GetForm() 
        {
            ViewData["Cities"] = new SelectList(await _cityRepository.GetByDefaults(x => x.Status != Status.Passive), "Id", "Name");
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> GetForm(CreateRequestFormDTO model)
        {
            ViewData["Cities"] = new SelectList(await _cityRepository.GetByDefaults(x => x.Status != Status.Passive), "Id", "Name");

            if (ModelState.IsValid)
            {
                var village = await _villageRepository.GetById(model.VilaggeId);
                var city = await _cityRepository.GetById(village.CityId);
                var request = _mapper.Map<RequestForm>(model);
                request.City = city.Name;
                request.Mall = village.Name;
                await _requestFormRepository.AddAsync(request);
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Village(int cityId)
        {
            List<Village> avm = new List<Village>();

            // eğer ülke id = 1 ise türkiyenin illerini ekle
            if (cityId == 1)
            {
                avm.Add(new Village { Id = 1, Name = "CepaAVM" });
                avm.Add(new Village { Id = 2, Name = "KENTPARK" });
                avm.Add(new Village { Id = 3, Name = "BEYSUPARK" });
                avm.Add(new Village { Id = 4, Name = "ARCADİUM" });
                avm.Add(new Village { Id = 5, Name = "PANORA" });
            }

           
            return Json(Village);
        }
    }
}
