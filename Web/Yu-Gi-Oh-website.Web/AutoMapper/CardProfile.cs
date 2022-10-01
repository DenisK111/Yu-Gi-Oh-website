﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Services.Models;
using Yu_Gi_Oh_website.Web.Models;
using Yu_Gi_Oh_website.Web.Models.CardCollection;
using Yu_Gi_Oh_website.Web.Models.CardDetails;

namespace Yu_Gi_Oh_website.Web.AutoMapper
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {

            CreateMap<Card, CardDto>()
                .ForMember(x => x.Type, y => y.MapFrom(s => s.Type.Name))
                .ForMember(x => x.CardAttribute, y => y.MapFrom(s => s.CardAttribute!.Name))
                .ForMember(x => x.CardType, y => y.MapFrom(s => s.CardType.ToString()))
                .ForMember(x=>x.ExactCardType,y=>y.MapFrom(s=>s.ExactCardType.Name))
                .ForMember(x => x.CardImages, y => y.MapFrom(s => s.CardImages.Select(c => c.ImageUrl)));
            CreateMap<CardDto, CardDetailsViewModel>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
            CreateMap<Card, CardDisplayDto>()
                .ForMember(x => x.ImageUrl, y => y.MapFrom(s => s.CardImages.Select(c => c.ImageUrl).FirstOrDefault(x => x.EndsWith("1.jpg"))));
            CreateMap<CardDisplayDto, CardDisplayViewModel>();
            
            CreateMap<Cattegory, CattegoryDto>();


            CreateMap<SubCattegory, SubCattegoryDto>()
                .ForMember(x=>x.ModifiedOn, y=>y.MapFrom(s=>s.ModifiedOn.HasValue ? ((DateTime)s.ModifiedOn).ToString("g") : null))
                .ForMember(x=>x.ModifiedOn, y=>y.MapFrom(s=>s.LastThreadModifiedOn.HasValue ? ((DateTime)s.LastThreadModifiedOn).ToString("g") : null));

            CreateMap<SubCattegory, FullSubCattegoryDto>()
                .ForMember(x => x.Cattegory, y => y.MapFrom(s => s.Cattegory.Name));

            CreateMap<ForumThread, ForumThreadDisplayDto>()
                .ForMember(x => x.Author, y => y.MapFrom(s => s.Author.UserName))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn.HasValue ? ((DateTime)s.ModifiedOn).ToString("g") : null))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn.ToString("g")));

        }
    }
}
