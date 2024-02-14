using ApplicationCore_WebReklam.Entities.Abstract;

namespace WebReklam.Areas.Admin.Models
{
    public class AppUserVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }

    }
}
