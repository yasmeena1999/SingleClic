using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.DTOs
{
    public class userfollowlistDto
    {
        public string Name { get; set; }
        public ICollection<string> Followers { get; set; }
        public ICollection<string> Followings { get; set; }
    }
}
