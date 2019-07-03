using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zamowienia.Models;

namespace Zamowienia
{
    public interface IZamowienieRepository
    {
        Task<IEnumerable<Zamowienie>> GetAll();
        Task<Zamowienie> GetById(int id);
        Task<IEnumerable<Towary>> GetTowaryById(int id);
        int GetZamowienieByIdTowaru(int id);
        void Add(Zamowienie zamowienie);
        void AddTowar(int idZamowienia, Towary towar);
        void DeleteTowar(int idTowaru);
        void UsunZamowienie(int zamowienieId);
        Zamowienie Update(Zamowienie zamowienie);
    }
}
