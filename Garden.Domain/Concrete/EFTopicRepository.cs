using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Garden.Domain.Entities;
using Garden.Domain.Abstract;
using System.Data.Entity;

namespace Garden.Domain.Concrete
{
    public class EFTopicRepository : ITopicRepository
    {
        IEFDbContext context;
        //EFCategoryRepository category = new EFCategoryRepository();

        public EFTopicRepository(IEFDbContext db)
        {
            context = db;
        }

        public IDbSet<Topic> GetAll
        {
            get { return context.Topics; }
        }

        public void SaveTopic(Topic topic)
        {
            if (topic.TopicId == 0)
            {
                context.Topics.Add(topic);
            }
            else
            {
                Topic dbEntry = context.Topics.Find(topic.TopicId);
                if (dbEntry != null)
                {
                    dbEntry.TopicName = topic.TopicName;
                    dbEntry.ContentTopic = topic.ContentTopic;
                }
            }
            context.Save();
        }

        public void SaveMessage(string topicName, Message message)
        {

            if (message.TestMessage != "")
            {
                Topic dbEntry = context.Topics.FirstOrDefault(t => t.TopicName == topicName);
                if (dbEntry != null)
                {
                    dbEntry.Messages.Add(message);
                }

                context.Save();
            }
        }

        public Topic DeleteTopic(int topicId)
        {
            Topic dbEntry = context.Topics.Include(t => t.Messages/*требуется для каскадного удаления данных - "жадная загрузка"*/)
                .FirstOrDefault(t => t.TopicId == topicId);
            if (dbEntry != null)
            {
                context.Topics.Remove(dbEntry);

                context.Save();
            }
            return dbEntry;
        }
    }
}