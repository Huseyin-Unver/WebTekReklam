using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.AccountDTO
{
    public class RegisterDTO
    {
        //[Display(Name = "Kullanıcı Adı")]
        //public string UserName { get; set; }
        [Display(Name = "Telefon numarası")]

        public string PhoneNumber { get; set; }

        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string LastName { get; set; }


        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}
