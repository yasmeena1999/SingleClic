using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.Context
{
    public class BloggingContext :IdentityDbContext<User>
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
         : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> UserFollowers { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>()
                 .HasOne(c => c.User)
                 .WithMany(u => u.Comments)
                 .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Follow>()
             .HasOne(uf => uf.Follower)
             .WithMany(u => u.Followees)
             .HasForeignKey(uf => uf.FollowerId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(uf => uf.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
