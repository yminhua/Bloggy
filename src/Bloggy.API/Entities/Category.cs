using System.Collections.Generic;

namespace Bloggy.API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post> ();
    }
}
