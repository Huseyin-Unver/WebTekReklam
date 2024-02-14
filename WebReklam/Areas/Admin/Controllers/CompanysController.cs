using ApplicationCore_WebReklam.DTO_s.CompanyDTO;
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
    public class CompanysController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompanysController(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var company = await _companyRepository.GetFilteredList
                (
                    select: x => new GetCompanyVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Konum = x.Konum,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != ApplicationCore_WebReklam.Entities.Abstract.Status.Passive
                );

            return View(company);
        }
        public IActionResult CreateCompany() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDTO model)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(model);
                await _companyRepository.AddAsync(company);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> UpdateCompany(int id)
        {
            if (id > 0)
            {
                var company = await _companyRepository.GetById(id);
                if (company != null)
                {
                    var model = _mapper.Map<UpdateCompanyDTO>(company);
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyDTO model)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(model);
                await _companyRepository.UpdateAsync(company);
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (id > 0)
            {
                var company = await _companyRepository.GetById(id);
                if (company is not null)
                {
                    await _companyRepository.DeleteAsync(company);
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
    }
}
