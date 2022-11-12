using AutoMapper;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Web.AutoMapper
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            CreateMap<Cattegory, CattegoryDto>();


            CreateMap<SubCattegory, SubCattegoryDto>()
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn.HasValue ? ((DateTime)s.ModifiedOn).ToString("g") : null))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.LastThreadModifiedOn.HasValue ? ((DateTime)s.LastThreadModifiedOn).ToString("g") : null));

            CreateMap<SubCattegory, FullSubCattegoryDto>()
                .ForMember(x => x.Cattegory, y => y.MapFrom(s => s.Cattegory.Name));

            CreateMap<ForumThread, ForumThreadDisplayDto>()
                .ForMember(x => x.Author, y => y.MapFrom(s => s.Author.UserName))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn.ToString("g")))
                .ForMember(x => x.PostsCount, y => y.MapFrom(s => s.Posts.Count()))
                .ForMember(x => x.LastModifiedBy, y => y.MapFrom(s => s.Posts.OrderByDescending(x => x.ModifiedOn).FirstOrDefault()!.Author.UserName));

            CreateMap<ForumThreadDisplayDto, ForumThreadDisplayViewModel>()
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn.HasValue ? ((DateTime)s.ModifiedOn).ToString("g") : null));

            CreateMap<ForumThread, ThreadDto>()
                .ForMember(x => x.Author, y => y.MapFrom(s => s.Author.UserName))
                .ForMember(x => x.SubCattegory, y => y.MapFrom(s => s.SubCattegory.Name))
                .ForMember(x => x.ModifiedOn, y => y.MapFrom(s => s.ModifiedOn.HasValue ? ((DateTime)s.ModifiedOn).ToString("g") : null))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn.ToString("g")));

            CreateMap<Post, PostDto>()
                .ForMember(x => x.Author, y => y.MapFrom(s => s.Author.UserName))
                .ForMember(x => x.AuthorCreatedOn, y => y.MapFrom(s => s.Author.CreatedOn.ToString("d")))
                .ForMember(x => x.PostContent, y => y.MapFrom(s => s.PostContent.Content))
                .ForMember(x => x.Likes, y => y.MapFrom(s => s.Votes.Where(x => x.IsUpvote).Count()))
                .ForMember(x => x.Dislikes, y => y.MapFrom(s => s.Votes.Where(x => !x.IsUpvote).Count()))
                .ForMember(x => x.AuthorPostsCount, y => y.MapFrom(s => s.Author.PostCount))
                .ForMember(x => x.CreatedOn, y => y.MapFrom(s => s.CreatedOn.ToString("g")));// TODO DO THE MAPPING

            CreateMap<ThreadDto, ThreadViewModel>();
            CreateMap<ThreadInfoDto, ThreadInfoViewModel>();
            CreateMap<FullSubCattegoryDto, FullSubCattegoryViewModel>();
        }
    }
}
