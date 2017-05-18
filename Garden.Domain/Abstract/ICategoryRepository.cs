using System.Collections.Generic;
using Garden.Domain.Entities;

namespace Garden.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll { get; }
        void AddTopic(Topic topic, string categoryName);
    }
}
