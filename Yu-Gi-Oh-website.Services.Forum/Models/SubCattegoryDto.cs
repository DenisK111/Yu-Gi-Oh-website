using Yu_Gi_Oh_website.Common;

namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public record SubCattegoryDto
    {
                public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string? ModifiedOn { get; set; }
        public string? LastThreadName { get; set; }
        public string? LastPostAuthor { get; set; }
        public string? LastThreadModifiedOn { get; set; }

        public string Description { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public int Id { get; set; }
    }
}