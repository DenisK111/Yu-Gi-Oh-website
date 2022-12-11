namespace Yu_Gi_Oh_website.Models.Forum.Models
{
	public class VisitorCount
	{
		public VisitorCount()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }

		public string Path { get; set; } = null!;
		public int ThreadId { get; set; }

		public ICollection<string> IpAddresses { get; set; } = null!;
	}
}
