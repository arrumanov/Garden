using System.Collections.Generic;
using Garden.Domain.Entities;

namespace Garden.Domain.Abstract
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAll { get; }
    }
}
