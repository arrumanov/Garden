using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Garden.Domain.Entities;
using System.Data.Entity;
using Garden.Domain.Abstract;

namespace Garden.Domain.Concrete
{

    //Паттерн Unit of Work позволяет упростить работу с различными 
    //репозиториями и дает уверенность, что все репозитории будут 
    //использовать один и тот же контекст данных
    public class UnitOfWorkEFDbContext : IDisposable, IUoWEFDbContext
    {
        private IEFDbContext db = new EFDbContext();
        private ICategoryRepository categoryRepository;
        private ITopicRepository topicRepository;
        private IMessageRepository messageRepository;

        public ICategoryRepository Categories
        {
            get
            {
                return categoryRepository;
            }
        }

        public ITopicRepository Topics
        {
            get
            {
                return topicRepository;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                return messageRepository;
            }
        }

        private bool disposed = false;


        public UnitOfWorkEFDbContext(ICategoryRepository categoryRepository,
            ITopicRepository topicRepository, IMessageRepository messageRepository)
        {
            this.categoryRepository = categoryRepository;
            this.topicRepository = topicRepository;
            this.messageRepository = messageRepository;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return db.Save();
        }
    }

    public interface IUoWEFDbContext
    {
        ICategoryRepository Categories { get; }
        ITopicRepository Topics { get; }
        IMessageRepository Messages { get; }

        int SaveChanges();

    }
}