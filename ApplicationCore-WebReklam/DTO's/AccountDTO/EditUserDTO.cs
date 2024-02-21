using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore_WebReklam.DTO_s.AccountDTO
{
    public class EditUserDTO
    {
      

        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        
       

        [Display(Name = "Telefon numarası")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

       
        public EditUserDTO() { }

        public EditUserDTO(AppUser user)
        {
            Email = user.UserName;
         
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
