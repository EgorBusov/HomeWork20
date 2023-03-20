using HomeWork20.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork20.Context
{
    public class HomeWork20Context : DbContext
    {
        public DbSet<Persone> Persones { get; set; }

        public HomeWork20Context(DbContextOptions<HomeWork20Context> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
