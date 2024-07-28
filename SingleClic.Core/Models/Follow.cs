using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public User Follower { get; set; }

        public string FolloweeId { get; set; }
        public User Followee { get; set; }
    }
}
