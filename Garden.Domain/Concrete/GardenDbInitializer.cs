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
    public class GardenDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            Message message1 = new Message() { TestMessage = "Message 1", Date = DateTime.Now };
            Message message2 = new Message() { TestMessage = "Message 2", Date = DateTime.Now };
            Message message3 = new Message() { TestMessage = "Message 3", Date = DateTime.Now };
            Message message4 = new Message() { TestMessage = "Message 4", Date = DateTime.Now };
            Message message5 = new Message() { TestMessage = "Message 5", Date = DateTime.Now };
            Message message6 = new Message() { TestMessage = "Message 6", Date = DateTime.Now };
            List<Message> listMessages1 = new List<Message>();
            List<Message> listMessages2 = new List<Message>();
            List<Message> listMessages3 = new List<Message>();
            List<Message> listMessages4 = new List<Message>();
            listMessages1.Add(message1);
            listMessages2.Add(message2);
            listMessages2.Add(message3);
            listMessages3.Add(message4);
            listMessages3.Add(message5);
            listMessages4.Add(message6);

            string contentTopic1 = "<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.<p><img src=\"../../Images/fon.png\" alt=\"Image\" /></p></p>";
            contentTopic1 += contentTopic1 += contentTopic1;
            string contentTopic2 = contentTopic1;
            string contentTopic3 = contentTopic1;
            string contentTopic4 = contentTopic1;

            Topic topic1 = new Topic() { TopicName = "Bla1", ContentTopic = contentTopic1, Messages = null };
            Topic topic2 = new Topic() { TopicName = "Bla2", ContentTopic = contentTopic2, Messages = listMessages2 };
            Topic topic3 = new Topic() { TopicName = "Bla3", ContentTopic = contentTopic3, Messages = listMessages3/*.AsQueryable()*/ };
            Topic topic4 = new Topic() { TopicName = "Bla4", ContentTopic = contentTopic4, Messages = null };
            Topic topic5 = new Topic() { TopicName = "Bla5", ContentTopic = contentTopic4, Messages = listMessages1 };
            Topic topic6 = new Topic() { TopicName = "Bla6", ContentTopic = contentTopic4, Messages = listMessages4 };
            List<Topic> listTopics1 = new List<Topic>();
            List<Topic> listTopics2 = new List<Topic>();
            List<Topic> listTopics3 = new List<Topic>();
            listTopics1.Add(topic1);
            listTopics1.Add(topic2);
            listTopics2.Add(topic3);
            listTopics2.Add(topic5);
            listTopics2.Add(topic6);
            listTopics3.Add(topic4);

            Category category1 = new Category() { CategoryName = "Томаты", Topics = listTopics1 };
            Category category2 = new Category() { CategoryName = "Селекция томатов", Topics = listTopics2 };
            Category category3 = new Category() { CategoryName = "Виноград", Topics = listTopics3 };
            Category category4 = new Category() { CategoryName = "Сад" };
            
            context.Messages.Add(message1);
            context.Messages.Add(message2);
            context.Messages.Add(message3);
            context.Messages.Add(message4);
            context.Messages.Add(message5);
            context.Messages.Add(message6);
            context.Topics.Add(topic1);
            context.Topics.Add(topic2);
            context.Topics.Add(topic3);
            context.Topics.Add(topic4);
            context.Topics.Add(topic5);
            context.Topics.Add(topic6);
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Categories.Add(category4);

            base.Seed(context);
        }
    }
}