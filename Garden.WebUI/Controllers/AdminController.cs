using System.Web.Mvc;
using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using System.Data.Entity;
using System.Linq;
using Microsoft.Security.Application;
using Garden.Domain.Concrete;

namespace Garden.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IUoWEFDbContext unitOfWork;

        //библиотека Ninject конструктор объявляет зависимость
        //от интерфейса ICategoryRepository
        public AdminController(ICategoryRepository categoryRepository,
            ITopicRepository topicRepository, IMessageRepository messageRepository)
        {
            unitOfWork = new UnitOfWorkEFDbContext(categoryRepository, topicRepository, messageRepository);
        }

        // для отображения всех тем по категориям
        public ViewResult Index()
        {
            return View(unitOfWork.Categories.GetAll);
        }

        //для изменения выбранной темы
        public ViewResult Edit(int categoryId, int topicId)
        {
            Topic topic = unitOfWork.Categories.GetAll.FirstOrDefault(c => c.CategoryId == categoryId)
                .Topics.FirstOrDefault(t => t.TopicId == topicId);
            ViewBag.CategoryName = topic.Category.CategoryName;

            return View(topic);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        [ValidateInput(false)]//отключение валидации данных полученных от admin 
        //для возможности HTML разметки
        public ActionResult Edit(Topic topic/*, string categoryName*/)
        {
            if (ModelState.IsValid)
            {
                // Проверка данных с помощью библиотеки AntiXSS
                // Для удаления вредоностных script-ов
                topic.ContentTopic = Sanitizer.GetSafeHtmlFragment(topic.ContentTopic);
                topic.TopicName = Sanitizer.GetSafeHtmlFragment(topic.TopicName);

                unitOfWork.Topics.SaveTopic(topic);

                TempData["message"] = string.Format("Изменения в теме \"{0}\" были сохранены", topic.TopicName);
                return RedirectToAction("Index");
            }
            else
            {
                //если что-то не так со значениями данных
                return View(topic);
            }
        }

        //создание темы
        public ViewResult Create()
        {
            return View("Create", new Topic());
        }

        //создание темы
        [HttpPost]
        [ValidateInput(false)]//отключение валидации данных полученных от admin 
        //для возможности HTML разметки
        public ActionResult Create(Topic topic, string categoryName)
        {
            if (ModelState.IsValid)
            {
                // Проверка данных с помощью библиотеки AntiXSS
                // Для удаления вредоностных script-ов
                topic.ContentTopic = Sanitizer.GetSafeHtmlFragment(topic.ContentTopic);
                topic.TopicName = Sanitizer.GetSafeHtmlFragment(topic.TopicName);

                unitOfWork.Categories.AddTopic(topic, categoryName);

                TempData["message"] = string.Format("Тема \"{0}\" была сохранена", topic.TopicName);
                return RedirectToAction("Index");
            }
            else
            {
                //если что-то не так со значениями данных
                return View(topic);
            }
        }

        //удаление темы
        [HttpPost]
        public ActionResult Delete(int topicId/*, string categoryName*/)
        {
            Topic deletedTopic = unitOfWork.Topics.DeleteTopic(topicId);

            if (deletedTopic != null)
            {
                TempData["message"] = string.Format("Тема \"{0}\" была удалена",
                    deletedTopic.TopicName);
            }
            return RedirectToAction("Index");
        }
    }
}
