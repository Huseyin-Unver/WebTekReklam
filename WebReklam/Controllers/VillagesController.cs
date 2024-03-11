using Infrastructure_WebReklam.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebReklam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillagesController : ControllerBase
    {
        private readonly IVillageRepository _villageRepository;

        public VillagesController(IVillageRepository villageRepository)
        {
            _villageRepository = villageRepository;
        }
        [HttpGet("GetVillages/{cityId}")]
        public async Task<IActionResult> GetVillages(int cityId) 
        {
            var villages = await _villageRepository.GetByDefaults(x => x.CityId == cityId);
            return new JsonResult(villages);
        }
    }
}
