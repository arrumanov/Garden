using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using Garden.WebUI.Models;
using Garden.Domain.Concrete;

namespace Garden.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        IUoWEFDbContext unitOfWork;

        //указывает на то,  сколько тем должно отображаться на одной странице
        public int pageSize = 3;

        //библиотека Ninject конструктор объявляет зависимость
        //от интерфейса ICategoryRepository
        public CategoryController(ICategoryRepository categoryRepository,
            ITopicRepository topicRepository, IMessageRepository messageRepository)
        {
            unitOfWork = new UnitOfWorkEFDbContext(categoryRepository, topicRepository, messageRepository);
        }
        //
        // GET: /Category/

        //В аргументах передается название Category (для которой необходимо
        //вывести список Topic) и номер страницы которую необходимо открыть
        public ActionResult Index(string category, int page = 1)
        {
            //если название категории не передается
            if (category == null)
            {
                category = "Селекция томатов";
            }
            IEnumerable<Topic> repository = null;
            foreach (var c in unitOfWork.Categories.GetAll)
            {
                if (c.CategoryName == category)
                {
                    repository = c.Topics;
                    ViewBag.CategoryName = c.CategoryName;
                }
            }

            TopicIndexViewModel model = new TopicIndexViewModel
            {
                Topics = repository
                    //.Where(p => categoryName == null || p.Category == category)
                .OrderBy(topic => topic.TopicId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

    }
}