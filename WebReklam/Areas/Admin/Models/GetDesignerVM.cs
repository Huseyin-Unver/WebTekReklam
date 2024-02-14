using ApplicationCore_WebReklam.Entities.Abstract;

namespace WebReklam.Areas.Admin.Models
{
    public class GetDesignerVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Description { get; set; }
        public string ImageFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }

    }
}
