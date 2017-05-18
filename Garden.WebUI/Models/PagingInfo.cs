using System;

namespace Garden.WebUI.Models
{
    //модель представления
    //Это класс для передачи данных между контроллером и представлением
    public class PagingInfo
    {
        // Кол-во тем
        public int TotalItems { get; set; }

        // Кол-во тем на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}