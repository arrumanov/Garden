using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garden.Domain.Entities
{
    public class Category
    {
        [Display(Name = "Название темы")]
        [Required(ErrorMessage = "Пожалуйста, введите название темы")]
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public virtual ICollection<Topic> Topics { set; get; }

        public IEnumerator<Topic> GetEnumerator()
        {
            return Topics.GetEnumerator();
        }
    }
}
