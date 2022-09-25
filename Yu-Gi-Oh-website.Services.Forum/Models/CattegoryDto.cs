namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public record CattegoryDto
    {
        public CattegoryDto()
        {
            this.SubCattegories = new HashSet<SubCattegoryDto>();
        }

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public ICollection<SubCattegoryDto> SubCattegories { get; set; }
    }
}