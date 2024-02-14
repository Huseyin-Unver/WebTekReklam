using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.CityDTO
{
    public class CreateCityDTO
    {
        [Required(ErrorMessage = "Şehir adı zorunludur..")]
        public string Name { get; set; }

    }
}
