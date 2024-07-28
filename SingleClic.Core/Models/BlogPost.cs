using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
