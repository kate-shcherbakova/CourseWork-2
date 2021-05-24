using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcAvia.Data;
using MvcAvia.Models;

namespace MvcAvia.Controllers
{
    public class SearchController : Controller
    {
        private readonly MvcAviaContext _context;

        public SearchController(MvcAviaContext context)
        {
            _context = context;
        }

        // GET-запит на адресу https://localhost:{port}/Search
        // у результаті роботи Index викличе метод View, у який передасть об'єкт моделі SearchViewModel
        public async Task<IActionResult> Index(SearchViewModel searchViewModel)
        {
            // усі дані таблиці Search
            var search = _context.Search
                .OrderByDescending(c => c.Id)
                .Include(c => c.FromAirport)
                .Include(c => c.ToAirport)                 
            ;

            var searchVM = new SearchViewModel
            {
                // останні 10 шуканих квитків
                Searches = await search.Take(10).ToListAsync()
            };

            if (!ModelState.IsValid)
            {
                return View(searchVM);
            }

            Airport fromAirport = _context.Airport
                .Where(c =>
                        c.CityName.ToLower().Replace(" ", "") == searchViewModel.From.ToLower().Replace(" ", "") ||
                        c.Name.ToLower().Replace(" ", "") == searchViewModel.From.ToLower().Replace(" ", "") ||
                        c.Code.ToLower().Replace(" ", "") == searchViewModel.From.ToLower().Replace(" ", "")
                    ).FirstOrDefault();

            Airport toAirport = _context.Airport
                .Where(c =>
                        c.CityName.ToLower().Replace(" ", "") == searchViewModel.To.ToLower().Replace(" ", "") ||
                        c.Name.ToLower().Replace(" ", "") == searchViewModel.To.ToLower().Replace(" ", "") ||
                        c.Code.ToLower().Replace(" ", "") == searchViewModel.To.ToLower().Replace(" ", "")
                    ).FirstOrDefault();

            if (fromAirport == null)
            {
                ModelState.AddModelError("From", "Место отправления не найдено");

                return View(searchVM);
            }

            if (toAirport == null)
            {
                ModelState.AddModelError("To", "Место прибытия не найдено");

                return View(searchVM);
            }

            if (fromAirport == toAirport)
            {
                ModelState.AddModelError("From", "Места отправления и прибытия совпадают");

                return View(searchVM);
            }

            if (Convert.ToDateTime(searchViewModel.Date) < DateTime.Today)
            {
                ModelState.AddModelError("Date", "Дата должна быть не меньше сегодняшней");

                return View(searchVM);
            }

            if (fromAirport.CityName == "" || toAirport.CityName == "")
            {
                ModelState.AddModelError("Date", "Билет не найден. Попробуйте позже!");

                return View(searchVM);
            }

            // якщо дані введено правильно
            Search newSearch = new Search
            {
                FromAirport = fromAirport,
                ToAirport = toAirport,
                Date = searchViewModel.Date,
                Price = (decimal)GetRandomPrice()
            };

            // додавання запиту у таблицю Search
            _context.Search.Add(newSearch);
            _context.SaveChanges();

            // формування оновлених даних з останнім запитом
            var updatedSearch = _context.Search
                .OrderByDescending(c => c.Id)
                .Include(c => c.FromAirport)
                .Include(c => c.ToAirport)
            ;

            var updatedSearchVM = new SearchViewModel
            {
                // оновлені дані про останні шукані квитки з квитком, про який був запит
                Searches = await search.Take(10).ToListAsync(),

                // квиток, про який був запит
                LastSearch = newSearch
            };

            // усі дані сторінки після пошуку
            return View(updatedSearchVM);
            
        }

        private Double GetRandomPrice()
        {
            Random rnd = new Random();

            var randomInt = rnd.Next(500);

            var randomDouble = rnd.NextDouble();

            return randomInt + randomDouble;
        }
    }
}
