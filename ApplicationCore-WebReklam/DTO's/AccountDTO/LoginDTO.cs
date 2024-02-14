using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.AccountDTO
{
    public class LoginDTO
    {
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
