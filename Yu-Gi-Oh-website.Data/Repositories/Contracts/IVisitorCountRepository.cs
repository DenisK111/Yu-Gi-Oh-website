namespace Yu_Gi_Oh_website.Data.Repositories.Contracts
{
	public interface IVisitorCountRepository
	{
		Task AddOrUpdateAsync(string path, string ipAddress,int threadId);
		Task<Dictionary<int,int>> GetTotalCountByThreadIdsAsync(IEnumerable<int> threadIds);
		Task<int> GetTotalCountByPathAsync(int threadID);

    }
}
