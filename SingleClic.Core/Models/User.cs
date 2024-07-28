using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.Models
{
    public class User : IdentityUser
    {
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followees { get; set; }
    }
}
