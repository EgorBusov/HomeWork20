using HomeWork20.AuthModels;
using HomeWork20.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeWork20.Context
{
    public class HomeWork20Context : IdentityDbContext<User>
    {
        public HomeWork20Context(DbContextOptions<HomeWork20Context> options) : base(options) { }

        /// <summary>
        /// Коллекция записей в телефонной книге
        /// </summary>
        public DbSet<Persone> Persones { get; set; }
    }
}
