using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garden.Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string TestMessage { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public virtual Topic Topic { get; set; }
    }
}
