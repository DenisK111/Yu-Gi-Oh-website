using Yu_Gi_Oh_website.Data.Repositories.Contracts;
using Yu_Gi_Oh_website.Data.Repositories.Implementations;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Services.Implementations;

namespace Yu_Gi_Oh_website.Web.Extentension
{
	public static class ServiceExtensions
	{
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddTransient<IGetApiDataAndUpdateDbService, GetApiDataAndUpdateDbService>()
                .AddTransient<ICardCollectionService, CardCollectionService>()
                .AddScoped<IFilterService, FilterService>()
                .AddScoped<ISortingService, SortingService>()
                .AddScoped<IHomePageService, HomePageService>()
                .AddScoped<ISubCattegoryService, SubCattegoryService>()
                .AddScoped<IThreadService, ThreadService>()
                .AddScoped<ISoftDeleteService<PostVote>, SoftDeleteService<PostVote>>()                
                .AddScoped<ISoftDeleteService<Post>, SoftDeleteService<Post>>()                
                .AddScoped<IPostService, PostService>()
                .AddScoped<IEntityByIdService, EntityByIdService>()
                .AddScoped<IVotesService, VotesService>()
                .AddScoped<IVisitorCountService,VisitorCountService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddSingleton<IVisitorCountRepository, MongoVisitorCountRepository>();

            return services;
        }


    }
}
