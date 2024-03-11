using System.ComponentModel.DataAnnotations;

namespace WebReklam.Model
{
    public class RequestFormVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Desciption { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [Display(Name = "Şehir")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [Display(Name = "Avmler")]
        public int AvmId
        {
            get; set;
        }
    }
}
