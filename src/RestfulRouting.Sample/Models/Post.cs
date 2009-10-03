namespace RestfulRouting.Sample.Models
{
	public class Post : Entity
	{
		public string Title { get; set; }
		public string Slug { get; set; }

		public string Body { get; set; }
	}
}