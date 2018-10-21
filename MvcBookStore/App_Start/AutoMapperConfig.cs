using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MvcBookStore.Models;
using MvcBookStore.ViewModels;

namespace MvcBookStore
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.CreateMap<Author, AuthorViewModel>().ReverseMap();
                cfg.CreateMap<List<Author>, List<AuthorViewModel>>().ReverseMap();
                //cfg.CreateMap<AuthorViewModel, Author>();
                //cfg.CreateMap<List<AuthorViewModel>, List<Author>>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}