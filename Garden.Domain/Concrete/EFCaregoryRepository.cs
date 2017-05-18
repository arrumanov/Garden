using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Garden.Domain.Entities;
using Garden.Domain.Abstract;

namespace Garden.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        public IEFDbContext context;

        public EFCategoryRepository(IEFDbContext db)
        {
            context = db;
        }

        public IEnumerable<Category> GetAll
        {
            get { return context.Categories.Include(c => c.Topics).ToList(); }
        }

        public void AddTopic(Topic topic, string categoryName)
        {

            Category dbEntry = context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (dbEntry != null)
            {
                dbEntry.Topics.Add(topic);
            }

            context.Save();
        }
    }
}