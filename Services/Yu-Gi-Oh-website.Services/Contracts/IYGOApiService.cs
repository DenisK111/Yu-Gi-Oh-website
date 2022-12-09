namespace Yu_Gi_Oh_website.Services.ApiService

{
    public interface IYGOApiService
    {
        Task AddAllCardsToDbAsync(string imageFolder);        
    }
}