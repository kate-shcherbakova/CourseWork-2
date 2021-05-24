using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAvia.Models
{
    // сутність аеропорт
    public class Airport
    {
        public int Id { get; set; }

        // назва аеропорту, наприклад "Хитроу"
        public string Name { get; set; }
        // назва міста, у якому є аеропорт, наприклад "Лондон"
        public string CityName { get; set; }

        // IATA код аеропорту, наприклад "LHR"
        public string Code { get; set; }
    }
}
