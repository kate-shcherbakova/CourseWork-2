using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcAvia.Models
{
    // сутність, що описує усі дані сторінки після успішного пошуку квитка
    public class SearchViewModel
    {
        // для відображення дати до її введення 
        public SearchViewModel()
        {
            Date = DateTime.Now;
        }

        // звідки
        [Required(ErrorMessage = "Введите данные про место отправления")]
        [StringLength(50, MinimumLength = 3)]
        public string From { get; set; }

        // куди
        [Required(ErrorMessage = "Введите данные про место прибытия")]
        [StringLength(50, MinimumLength = 3)]
        public string To { get; set; }

        // дата
        [Required(ErrorMessage = "Введите данные про дату отправления")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // список даних пошуків користувачів - з нього беруться останні пошуки
        public List<Search> Searches { get; set; }

        // дані про останній пошук - квиток, що відображається
        public Search LastSearch { get; set; }
    }
}
