using ApplicationCore_WebReklam.DTO_s.AccountDTO;
using ApplicationCore_WebReklam.DTO_s.CityDTO;
using ApplicationCore_WebReklam.DTO_s.CompanyDTO;
using ApplicationCore_WebReklam.DTO_s.DesignerDTO;
using ApplicationCore_WebReklam.DTO_s.RequestFormDTO;
using ApplicationCore_WebReklam.DTO_s.VillageDTO;
using ApplicationCore_WebReklam.Entities.Concrete;
using ApplicationCore_WebReklam.Entities.UserEntities.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_WebReklam.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<City, CreateCityDTO>().ReverseMap();
            CreateMap<City, UpdateCityDTO>().ReverseMap();
            CreateMap<Village, CreateVillageDTO>().ReverseMap();
            CreateMap<Village, UpdateVillageDTO>().ReverseMap();
            CreateMap<Designer, CreateDesignerDTO>().ReverseMap();
            CreateMap<Designer, UpdateDesignerDTO>().ReverseMap();
            CreateMap<Company, UpdateCompanyDTO>().ReverseMap();
            CreateMap<Company, CreateCompanyDTO>().ReverseMap();
            CreateMap<RequestForm, CreateRequestFormDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, EditUserDTO>().ReverseMap();





        }
    }
}
