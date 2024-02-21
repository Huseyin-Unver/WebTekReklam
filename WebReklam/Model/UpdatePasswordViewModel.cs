using System.ComponentModel.DataAnnotations;

namespace WebReklam.Model
{
    public class UpdatePasswordViewModel
    {
       
            [Display(Name = "Yeni Şifre")]
            [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}
