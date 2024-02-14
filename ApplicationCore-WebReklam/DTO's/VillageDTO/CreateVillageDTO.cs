using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.VillageDTO
{
    public class CreateVillageDTO
    {
        [Required(ErrorMessage = "İlçe adı zorunludur..")]
        public string Name { get; set; }
    }
}
