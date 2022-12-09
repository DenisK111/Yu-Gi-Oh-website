namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class ThreadInfoDto
    {
        public int Id { get; set; }

        public int SubCattegoryId { get; set; }

        public string SubCattegorySlug { get; set; } = null!;

        public bool IsError { get; set; }

        public string? ErrorMessage { get; set; }

        public int CurrentPage { get; set; }

    }
}
