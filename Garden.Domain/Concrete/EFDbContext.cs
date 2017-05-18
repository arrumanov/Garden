using System.Collections.Generic;
using Garden.Domain.Entities;
using System.Data.Entity;
using System;

namespace Garden.Domain.Concrete
{
    public class EFDbContext : DbContext, IEFDbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public EFDbContext()
            : base("GardenDb")
        { }

        //// Отражение таблиц базы данных на свойства с типом DbSet
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Topic> Topics { get; set; }
        //public DbSet<Message> Messages { get; set; }

        public IDbSet<Category> /*IEFDbContext.*/Categories { get; set; }

        public IDbSet<Topic> Topics { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public int Save()
        {
            return this.SaveChanges();
        }
    }

    public interface IEFDbContext : IDisposable
    {
        IDbSet<Category> Categories { get; set; }
        IDbSet<Topic> Topics { get; set; }
        IDbSet<Message> Messages { get; set; }

        int Save();

    }
}

