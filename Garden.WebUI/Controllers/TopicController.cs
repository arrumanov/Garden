using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using System.Data.Entity;
using Garden.Domain.Concrete;
using Microsoft.Security.Application;

namespace Garden.WebUI.Controllers
{
    public class TopicController : Controller
    {
        IUoWEFDbContext unitOfWork;

        //библиотека Ninject конструктор объявляет зависимость
        //от интерфейса ITopicRepository
        public TopicController(ICategoryRepository categoryRepository,
            ITopicRepository topicRepository, IMessageRepository messageRepository)
        {
            unitOfWork = new UnitOfWorkEFDbContext(categoryRepository, topicRepository, messageRepository);
        }
        //
        // GET: /Topic/

        // пердается id темы, которую необходимо отобразить
        public ActionResult Index(int id, string category)
        {
            ViewBag.CategoryOfTopic = category;

            Topic result = unitOfWork.Topics.GetAll.Include(t => t.Messages).FirstOrDefault(t => t.TopicId == id);

            if (result != null)
            {
                return View(result);
            }


            // в будущем надо в этом случае переводить на сраницу Error
            return View(result);
        }

        public ActionResult NewMessage(string topicName, string newMessage, int topicId)
        {
            // Проверка данных с помощью библиотеки AntiXSS
            // Для удаления вредоностных script-ов
            newMessage = Sanitizer.GetSafeHtmlFragment(newMessage);

            Message message = new Message() { TestMessage = newMessage, Date = DateTime.Now };
            unitOfWork.Topics.SaveMessage(topicName, message);
            ModelState.Clear();
            Topic result = unitOfWork.Topics.GetAll.Include(t => t.Messages).FirstOrDefault(t => t.TopicId == topicId);
            //если отключен JS
            if (Request.IsAjaxRequest())
            {
                return PartialView(result);
            }

            return View(result);
        }

    }
}
