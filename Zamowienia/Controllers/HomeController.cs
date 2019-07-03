using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Zamowienia.Models;

namespace Zamowienia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IZamowienieRepository _zamowienieRepository;

        public HomeController(IZamowienieRepository zamowienieRepository)
        {
            _zamowienieRepository = zamowienieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Zamowienie> zamowienie = await _zamowienieRepository.GetAll();

            return View(zamowienie);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Zamowienie zamowienie = await _zamowienieRepository.GetById(id);
            IEnumerable<Towary> towary = await _zamowienieRepository.GetTowaryById(id);

            ZamowenieTowaryView model = new ZamowenieTowaryView
            {
                Zamowienie = zamowienie,
                Towary = towary
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Details(ZamowenieTowaryView model)
        {
            var idZamowienia = model.Zamowienie.ZamowienieID;
            var towar = model.nowyTowar;
            _zamowienieRepository.AddTowar(idZamowienia, towar);

            return RedirectToAction("Details", new { id = model.Zamowienie.ZamowienieID });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Zamowienie model)
        {
            if (ModelState.IsValid)
            {
                Zamowienie zamowienie = new Zamowienie()
                {
                    Nazwa = model.Nazwa,
                    DataZlozenia = DateTime.Now                  
                };

                _zamowienieRepository.Add(zamowienie);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {

            _zamowienieRepository.UsunZamowienie(id);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTowar(int id)
        {
            int idZamowienia = _zamowienieRepository.GetZamowienieByIdTowaru(id);
            _zamowienieRepository.DeleteTowar(id);

            return RedirectToAction("Details", new { id = idZamowienia });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Zamowienie zamowienie = await _zamowienieRepository.GetById(id);

            return View(zamowienie);
        }

        [HttpPost]
        public IActionResult Update(Zamowienie model)
        {
            if (ModelState.IsValid)
            {
                _zamowienieRepository.Update(model);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
