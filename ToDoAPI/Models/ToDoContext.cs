using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=ToDo;Username=fernandn;Password=n0542648n");
    }
}
