using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Entity;

namespace TechBaraholka.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "AdminName",
                Surname = "AdminSurname",
                Email = "admin@mail.ru",
                Password = "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70",
                Address = "admin street",
                Role = "Admin",
                AvatarPath = "/AvatarPics/DefaultAvatar.png",
                Balance = 0
            });
        }
    }
}
