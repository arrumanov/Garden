using System.Collections.Generic;
using Garden.Domain.Entities;

namespace Garden.WebUI.Models
{
    //требуется для предоставления представлению из контроллера экземпляза 
    //класса модели представления PagingIndo (который содержит информащию
    //о разбиении на страницы) вместе с Topics
    public class TopicIndexViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
        public PagingInfo PagingInfo { get; set; }

        //название передаваемой в представление категории
        public string CurrentCategory { get; set; }
    }
}