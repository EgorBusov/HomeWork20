using HomeWork20.Models;

namespace HomeWork20.Interfaces
{
    public interface IPersoneData
    {
        IEnumerable<Persone> GetPersones();
        Persone GetOnePersone(int id);
        void AddPersone(Persone persone);
        void DeletePersone(int personeId);
        void EditPersone(Persone persone);
    }
}
