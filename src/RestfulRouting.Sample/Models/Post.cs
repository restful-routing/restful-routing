namespace RestfulRouting.Sample.Models
{
	public class Post : Entity
	{
		public Blog Blog { get; set; }

		public string Title { get; set; }
		public string Slug { get; set; }

		public string Body { get; set; }
	}
}