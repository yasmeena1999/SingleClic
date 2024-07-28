using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.Responses
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string Id { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
