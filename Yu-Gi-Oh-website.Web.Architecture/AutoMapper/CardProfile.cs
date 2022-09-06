using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Services.AutoMapper
{
	public class CardProfile : Profile
	{
		public CardProfile()
		{
			
			this.CreateMap<Card, CardDto>()
				.ForMember(x => x.Race, y => y.MapFrom(s => s.Race.Name))
				.ForMember(x => x.CardAttribute, y => y.MapFrom(s => s.CardAttribute!.Name))
				.ForMember(x => x.CardType, y => y.MapFrom(s => s.CardType.Name))
				.ForMember(x => x.CardImages, y => y.MapFrom(s => s.CardImages.Select(c => c.ImageUrl)));
			this.CreateMap<CardDto, CardViewModel>();
			// Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
			this.CreateMap<Card,CardDisplayDto>()
				.ForMember(x=>x.ImageUrl,y=>y.MapFrom(s=>s.CardImages.Select(c => c.ImageUrl).FirstOrDefault(x=>x.EndsWith("1.jpg"))));
			this.CreateMap<CardDisplayDto, CardDisplayViewModel>();


		}
	}
}
