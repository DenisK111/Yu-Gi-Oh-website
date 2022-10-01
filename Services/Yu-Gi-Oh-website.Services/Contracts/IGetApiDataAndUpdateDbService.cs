namespace Yu_Gi_Oh_website.Services.ApiService

{
    public interface IGetApiDataAndUpdateDbService
    {
        Task AddAllCardsToDbAsync(string imageFolder);
        Task AddIndividualCardToDbAsync(string imageFolder, string cardName);
    }
}