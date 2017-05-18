using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Garden.Domain.Entities;
using Garden.Domain.Abstract;

namespace Garden.Domain.Concrete
{
    public class EFMessageRepository : IMessageRepository
    {
        IEFDbContext context;

        public EFMessageRepository(IEFDbContext db)
        {
            context = db;
        }

        public IEnumerable<Message> GetAll
        {
            get { return context.Messages; }
        }
    }
}
