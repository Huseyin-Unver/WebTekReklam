using ApplicationCore_WebReklam.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.Entities.Concrete
{
    public class City : BaseEntity
    {
     
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<Village> Avm { get; set; }

    }
}
