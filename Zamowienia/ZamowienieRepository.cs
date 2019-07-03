using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Zamowienia.Models;

namespace Zamowienia.Views
{
    public class ZamowienieRepository : IZamowienieRepository
    {
        public async Task<IEnumerable<Zamowienie>> GetAll()
        {
            IEnumerable<Zamowienie> listaZamowien;

            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                await connection.OpenAsync();
                var query = @"SELECT ZamowienieID, Nazwa, DataZlozenia, DataZakonczenia FROM Zamowienia";

                listaZamowien = await connection.QueryAsync<Zamowienie>(query);
            }
            return listaZamowien;
        }

        public async Task<Zamowienie> GetById(int id)
        {
            Zamowienie zamowienie;

            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                await connection.OpenAsync();
                var query = @"SELECT ZamowienieID, Nazwa, DataZlozenia, DataZakonczenia FROM Zamowienia WHERE ZamowienieID = @numer";
                zamowienie = await connection.QuerySingleAsync<Zamowienie>(query, new { numer = id });              
            }
            return zamowienie;
        }

        public async Task<IEnumerable<Towary>> GetTowaryById(int id)
        {
            IEnumerable<Towary> listaTowarow;

            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                await connection.OpenAsync();
                var query = @"SELECT TowaryID, Nazwa, Ilosc FROM Towary WHERE Zamowienia = @numer";
                listaTowarow = await connection.QueryAsync<Towary>(query, new { numer = id });
            }

            return listaTowarow;
        }

        public void Add(Zamowienie zamowienie)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.OpenAsync();
                var query = "INSERT INTO Zamowienia (Nazwa, DataZlozenia) VALUES (@NazwaZamowienia, @Data);";
                connection.Execute(query, new { NazwaZamowienia = zamowienie.Nazwa, Data = DateTime.Now });
            }
        }

        public void AddTowar(int idZamowienia, Towary towar)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.OpenAsync();
                var query = "INSERT INTO Towary (Nazwa, Ilosc, Zamowienia) VALUES (@Towar, @Liczba, @Zamowienie);";
                connection.Execute(query, new { Towar = towar.Nazwa, Liczba = towar.Ilosc, Zamowienie = idZamowienia });
            }
        }

        public void DeleteTowar(int idTowaru)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.OpenAsync();
                var query = "DELETE FROM Towary WHERE TowaryID = @Id;";
                connection.Execute(query, new { Id = idTowaru });
            }
        }

        public void UsunZamowienie(int zamowienieId)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.Open();
                var query = "DELETE FROM Towary WHERE Zamowienia = @Id;";
                connection.Execute(query, new { Id = zamowienieId });

                var query2 = "DELETE FROM Zamowienia WHERE ZamowienieID = @Id;";
                connection.Execute(query2, new { Id = zamowienieId });
            }
        }
         
        public Zamowienie Update(Zamowienie zamowienie)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.Open();
                var query = "UPDATE Zamowienia SET Nazwa = @NowaNazwa, DataZlozenia = @NowaData WHERE ZamowienieID = @Id;";
                connection.Execute(query, new { NowaNazwa = zamowienie.Nazwa, NowaData = zamowienie.DataZlozenia, Id = zamowienie.ZamowienieID });
            }

            return zamowienie;
        }

        public int GetZamowienieByIdTowaru(int towarID)
        {
            int towar;

            using (var connection = new SqlConnection("Server=localhost;Database=ZamowieniaDB;Trusted_Connection=True;"))
            {
                connection.Open();
                var query = "SELECT Zamowienia FROM Towary WHERE TowaryID = @Id";
                towar = connection.QuerySingle<int>(query, new { Id = towarID });
            }

            return towar;
        }
    }
}
