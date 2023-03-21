using HomeWork20.Context;
using HomeWork20.Interfaces;
using HomeWork20.Models;
using System.Net;

namespace HomeWork20.Data
{
    public class PersoneData : IPersoneData
    {
        private readonly HomeWork20Context context;

        public PersoneData(HomeWork20Context context)
        {
            this.context = context;
        }

        public void AddPersone(Persone persone)
        {
            context.Persones.Add(persone);

            context.SaveChanges();
        }

        public void DeletePersone(int personeId)
        {
            var persone = context.Persones.Where(e => e.Id == personeId).FirstOrDefault();

            context.Persones.Remove(persone);
            context.SaveChanges();
        }

        public void EditPersone(Persone persone)
        {
            var personeEdit = context.Persones.Where(e => e.Id == persone.Id).FirstOrDefault();
            personeEdit.Name = persone.Name;
            personeEdit.SurName = persone.SurName;
            personeEdit.FatherName = persone.FatherName;
            personeEdit.Telephone = persone.Telephone;
            personeEdit.Address = persone.Address;
            personeEdit.Description = persone.Description;

            context.SaveChanges();
        }

        public Persone GetOnePersone(int id)
        {
            return this.context.Persones.Where(e => e.Id == id).FirstOrDefault();
        }

        public IEnumerable<Persone> GetPersones()
        {
            return this.context.Persones;
        }
    }
}
