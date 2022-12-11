namespace Yu_Gi_Oh_website.Services.Forum.Models
{
    public class SubCattegoryInfoDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int CattegoryId { get; set; }

        public bool IsDeleted { get; set; }        
    }
}
