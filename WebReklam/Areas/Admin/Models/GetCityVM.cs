using ApplicationCore_WebReklam.Entities.Abstract;

namespace WebReklam.Areas.Admin.Models
{
    public class GetCityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
