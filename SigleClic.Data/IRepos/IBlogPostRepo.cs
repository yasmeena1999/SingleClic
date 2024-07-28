using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.IRepos
{
    public interface IBlogPostRepo : IRepository<BlogPost>
    {
        Task<IReadOnlyList<BlogPost>> SearchPostsAsync(string title, string authorname);
    }
}
