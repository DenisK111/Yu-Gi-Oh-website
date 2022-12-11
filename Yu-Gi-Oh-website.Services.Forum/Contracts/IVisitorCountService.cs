namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface IVisitorCountService
	{
        Task AddOrUpdateAsync(string path, string ipAddress,int threadId);
        Task<Dictionary<int,int>> GetTotalCountByThreadIdsAsync(IEnumerable<int> threadIds);
    }
}
