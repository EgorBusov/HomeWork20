using HomeWork20.Context;
using HomeWork20.Interfaces;
using HomeWork20.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HomeWork20.Data
{
    /// <summary>
    /// Работа с данными
    /// </summary>
    public class PersoneData : IPersoneData
    {
        private readonly HomeWork20Context context;

        public PersoneData(HomeWork20Context context)
        {
            this.context = context;
        }
       
        public async Task AddPersone(Persone persone)
        {
            context.Persones.Add(persone);

            await context.SaveChangesAsync();
        }
        
        public async Task DeletePersone(int personeId)
        {
            var persone = await context.Persones.FirstOrDefaultAsync(e => e.Id == personeId);

            context.Persones.Remove(persone);
            await context.SaveChangesAsync();
        }
        
        public async Task EditPersone(Persone persone)
        {
            var personeEdit = await context.Persones.FirstOrDefaultAsync(e => e.Id == persone.Id);
            personeEdit.Name = persone.Name;
            personeEdit.SurName = persone.SurName;
            personeEdit.FatherName = persone.FatherName;
            personeEdit.Telephone = persone.Telephone;
            personeEdit.Address = persone.Address;
            personeEdit.Description = persone.Description;

            await context.SaveChangesAsync();
        }

        public async Task<Persone> GetOnePersone(int id)
        {
            return await context.Persones.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<IEnumerable<Persone>> GetPersones()
        {
            return await context.Persones.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
