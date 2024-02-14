using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.AccountDTO
{
    public class EditUserDTO
    {
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        public EditUserDTO() { }

        public EditUserDTO(AppUser user)
        {
            UserName = user.UserName;
            Password = user.PasswordHash;
            Email = user.Email;
        }
    }
}

