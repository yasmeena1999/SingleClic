using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.DTOs
{
    public class PostRetrievedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ICollection<CommentAddedDto> Comments { get; set; }
    }
}
