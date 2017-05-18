using System.Collections.Generic;
using System.Data.Entity;
using Garden.Domain.Entities;

namespace Garden.Domain.Abstract
{
    public interface ITopicRepository
    {
        IDbSet<Topic> GetAll { get; }
        void SaveTopic(Topic topic);
        void SaveMessage(string topicName, Message message);
        Topic DeleteTopic(int topicId);
    }
}
