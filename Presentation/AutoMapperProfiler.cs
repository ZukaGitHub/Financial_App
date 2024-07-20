using AutoMapper;
using Domain.Entities;
using Presentation.Models;
using Presentation.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.AutoMapper
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<Client, CreateClientDTO>()
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
              .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));

            CreateMap<CreateClientDTO, Client>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));


            CreateMap<Address, AddressDTOForCreate>()
                .ReverseMap();


            CreateMap<Account, AccountDTOForCreate>()
                .ReverseMap();


        }


    }
}