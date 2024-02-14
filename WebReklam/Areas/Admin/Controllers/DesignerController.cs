using ApplicationCore_WebReklam.DTO_s.DesignerDTO;
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
    public class DesignersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDesignerRepository _designerRepository;

        public DesignersController(IMapper mapper, IDesignerRepository designerRepository)
        {
            _mapper = mapper;
            _designerRepository = designerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var designer = await _designerRepository.GetFilteredList
                (
                    select: x => new GetDesignerVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Description = x.Description,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        CreatedDate = x.CreatedDate,
                        Status = x.Status,
                        UpdatedDate = x.UpdatedDate
                    },
                    where: x => x.Status != ApplicationCore_WebReklam.Entities.Abstract.Status.Passive
                );
            return View(designer);
        }
        public IActionResult CreateDesigner() => View();

        [HttpPost]
        public async Task<IActionResult> CreateDesigner(CreateDesignerDTO model)
        {
            if (ModelState.IsValid)
            {
                var designer = _mapper.Map<Designer>(model);
                await _designerRepository.AddAsync(designer);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateDesigner(int id)
        {
            if (id > 0)
            {
                var designer = await _designerRepository.GetById(id);
                if (designer != null)
                {
                    var model = _mapper.Map<UpdateDesignerDTO>(designer);
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDesigner(UpdateDesignerDTO model)
        {
            if (ModelState.IsValid)
            {
                var designer = _mapper.Map<Designer>(model);
                await _designerRepository.UpdateAsync(designer);
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public async Task<IActionResult> DeleteDesigner(int id)
        {
            if (id > 0)
            {
                var designer = await _designerRepository.GetById(id);
                if (designer is not null)
                {
                    await _designerRepository.DeleteAsync(designer);
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
        public IActionResult UploadImage()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/");

            var imageList = Directory.GetFiles(path);

            List<GetDesignerVM> uploadedImages = new List<GetDesignerVM>();

            foreach (var image in imageList)
            {
                FileInfo fileInfo = new FileInfo(image);

                GetDesignerVM model = new GetDesignerVM();
                model.ImageFullName = image.Substring(image.IndexOf("wwwroot")).Replace("wwwroot/", string.Empty);


                uploadedImages.Add(model);
            }

            return View(uploadedImages);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);

                string imageName = Guid.NewGuid() + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");

                using var stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);

            }

            return RedirectToAction("UploadImage");
        }
    }
}
