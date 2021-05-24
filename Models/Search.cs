using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAvia.Models
{
    // сутність, що описує дані пошуку
    public class Search
    {
        public int Id { get; set; }
        
        // звідки
        [Display(Name = "Откуда")]
        public virtual Airport FromAirport { get; set; }
        
        // куди
        [Display(Name = "Куда")]
        public virtual Airport ToAirport { get; set; }

        // дата
        [Display(Name = "Дата вылета")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // ціна
        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
