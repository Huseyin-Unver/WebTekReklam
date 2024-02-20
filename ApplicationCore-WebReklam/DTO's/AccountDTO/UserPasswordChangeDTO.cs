using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.AccountDTO
{
    public class UserPasswordChangeDTO
    {
        [Display(Name = "Şuan ki Şifreniz")]
        [Required(ErrorMessage ="{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]

        public string CurrentPassword { get; set; }



        [Display(Name = "Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }


        [Display(Name = "Yeni Şifreniz tekrarı ")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Girdiğiniz yeni şifreler birbirleriyle uyuşmuyor.")]


        public string RepeatPassword { get; set; }
    }
}
