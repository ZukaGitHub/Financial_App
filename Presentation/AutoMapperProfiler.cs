using AutoMapper;
using Domain.Entities;
using Presentation.Models;
using Presentation.Models.Client;
using Presentation.Models.SearchEngine;
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
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            /*  .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts))*/

            CreateMap<CreateClientDTO, Client>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
                //.ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));
            CreateMap<UpdateClientDTO,Client>()
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src => src.Address));

            CreateMap<Address, AddressDTO>()
             .ReverseMap();
            CreateMap<SearchEngine, SearchEngineDTO>().ReverseMap();
            CreateMap<SearchEngine, CreateSearchEngineDTO>().ReverseMap();
            CreateMap<SearchEngine, UpdateSearchEngineDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>()
                .ReverseMap();
            CreateMap<Address, AddressDTOForCreate>()
                .ReverseMap();


            CreateMap<Account, AccountDTOForCreate>()
                .ReverseMap();


        }


    }
}