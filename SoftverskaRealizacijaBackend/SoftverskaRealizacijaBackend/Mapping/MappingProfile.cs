﻿using AutoMapper;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Models;

namespace SoftverskaRealizacijaBackend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Client, ClientDto>().ReverseMap();
           // CreateMap<Client, ClientDto>().ReverseMap();
            //CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}