using ApplicationCore_WebReklam.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.Entities.Concrete
{
    public class Village : BaseEntity
    {
      
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

    }
}
