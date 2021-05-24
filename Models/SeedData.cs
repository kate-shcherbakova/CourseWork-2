using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcAvia.Data;
using System;
using System.IO;
using System.Linq;

namespace MvcAvia.Models
{
    // для заповнення бази даних Airport при першому запуску програми
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcAviaContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcAviaContext>>()))
            {
                // Якщо дані вже є, базу даних Airport заповнювати не потрібно
                if (context.Airport.Any())
                {
                    return; 
                }

                string path = @"wwwroot/AirportsData.txt";

                string[] list = File.ReadAllLines(path);


                foreach (var el in list)
                {
                    var temp = el.Split('+');
                    string name = temp[2];
                    string cityName = temp[6];
                    string code = temp[0];

                    context.Airport.Add(new Airport
                    {
                        Name = name,
                        CityName = cityName,
                        Code = code
                    });
                }

                context.SaveChanges();
            }
        }
    }
}