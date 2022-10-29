namespace Yu_Gi_Oh_website.Models.BaseModels
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
