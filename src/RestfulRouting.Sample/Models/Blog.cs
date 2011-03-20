using System.Collections.Generic;

namespace RestfulRouting.Sample.Models
{
    public class Blog : Entity
    {
        public string Author { get; set; }

        public IList<Post> Posts { get; set; }
    }
}