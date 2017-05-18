using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using Garden.Domain.Concrete;

namespace Garden.WebUI.Controllers
{
    public class MessageController : Controller
    {
        IUoWEFDbContext unitOfWork;

        public MessageController(ICategoryRepository categoryRepository,
            ITopicRepository topicRepository, IMessageRepository messageRepository)
        {
            unitOfWork = new UnitOfWorkEFDbContext(categoryRepository, topicRepository, messageRepository);
        }
        //
        // GET: /Message/

        public ActionResult List()
        {
            return View(unitOfWork.Messages.GetAll);
        }

    }
}
