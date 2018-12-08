using System;
using System.Collections.Generic;
using System.Text;
using HomeLibrary.DatabaseModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
    }
}
