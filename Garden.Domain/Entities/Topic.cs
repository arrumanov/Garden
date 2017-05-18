using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garden.Domain.Entities
{
    public class Topic
    {
        [HiddenInput(DisplayValue = false)]
        public int TopicId { get; set; }

        [Display(Name = "Название темы")]
        [Required(ErrorMessage = "Пожалуйста, введите название темы")]
        public string TopicName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Содержание")]
        [Required(ErrorMessage = "Пожалуйста, введите содержание темы")]
        public string ContentTopic { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public IEnumerator<Message> GetEnumerator()
        {
            return Messages.GetEnumerator();
        }
    }
}
