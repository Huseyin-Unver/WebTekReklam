using ApplicationCore_WebReklam.DTO_s.VillageDTO;
using ApplicationCore_WebReklam.Entities.Concrete;
using AutoMapper;
using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebReklam.Areas.Admin.Models;

namespace WebReklam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class VillagesController : Controller
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IMapper _mapper;

        public VillagesController(IVillageRepository villageRepository, IMapper mapper)
        {
            _villageRepository = villageRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var villages = await _villageRepository.GetFilteredList
                (
                    select: x => new GetVillageVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != ApplicationCore_WebReklam.Entities.Abstract.Status.Passive
                );

            return View(villages);
        }

        public IActionResult CreateVillage() => View();

        [HttpPost]
        public async Task<IActionResult> CreateVillage(CreateVillageDTO model)
        {
            if (ModelState.IsValid)
            {
                var village = _mapper.Map<Village>(model);
                await _villageRepository.AddAsync(village);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> UpdateVillage(int id)
        {
            if (id > 0)
            {
                var village = await _villageRepository.GetById(id);
                if (village != null)
                {
                    var model = _mapper.Map<UpdateVillageDTO>(village);
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVillage(UpdateVillageDTO model)
        {
            if (ModelState.IsValid)
            {
                var village = _mapper.Map<Village>(model);
                await _villageRepository.UpdateAsync(village);
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public async Task<IActionResult> DeleteVillage(int id)
        {
            if (id > 0)
            {
                var village = await _villageRepository.GetById(id);
                if (village is not null)
                {
                    await _villageRepository.DeleteAsync(village);
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
    }
}
