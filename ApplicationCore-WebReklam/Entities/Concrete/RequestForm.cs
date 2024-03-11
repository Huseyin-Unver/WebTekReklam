using ApplicationCore_WebReklam.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.Entities.Concrete
{
    public class RequestForm : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }   
        public string CompanyName { get; set; }

        public string City { get; set; }
        public string Mall { get; set; }
        public string Message { get; set; }

    }
}
