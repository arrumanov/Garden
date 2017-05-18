using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using Garden.Domain.Concrete;
using Garden.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace Garden.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Category()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object, null, null);

            // Действие
            List<Category> result = ((IEnumerable<Category>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 4);
            Assert.AreEqual("Томаты", result[0].CategoryName);
            Assert.AreEqual("Селекция томатов", result[1].CategoryName);
            Assert.AreEqual("Виноград", result[2].CategoryName);
            Assert.AreEqual("Сад", result[3].CategoryName);
        }

        //Модульное тестирование: метод действия Edit()
        [TestMethod]
        public void Can_Edit_Topic()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object, null, null);

            // Действие
            Topic topic1 = controller.Edit(1, 7).ViewData.Model as Topic;
            Topic topic2 = controller.Edit(2, 3).ViewData.Model as Topic;
            Topic topic3 = controller.Edit(3, 8).ViewData.Model as Topic;
            Topic topic4 = controller.Edit(4, 8).ViewData.Model as Topic;

            // Assert
            Assert.AreEqual(7, topic1.TopicId);
            Assert.AreEqual(3, topic2.TopicId);
            Assert.AreEqual(8, topic3.TopicId);
            Assert.AreEqual(9, topic4.TopicId);
        }

        //Модульное тестирование: метод действия Edit()
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Topic()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object, null, null);

            // Действие
            Topic result = controller.Edit(2, 7).ViewData.Model as Topic;

            // Assert
        }

        //Модульное тестирование: отправки, связанные с редактированием
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ITopicRepository> mock = new Mock<ITopicRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(null, mock.Object, null);

            // Организация - создание объекта Topic
            Topic topic = new Topic
            {
                TopicId = 0,
                TopicName = "Bla0",
                ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>",
                Messages = null
            };

            // Действие - попытка сохранения темы
            ActionResult result = controller.Edit(topic);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveTopic(topic));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        //Модульное тестирование: отправки, связанные с редактированием
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<ITopicRepository> mock = new Mock<ITopicRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(null, mock.Object, null);

            // Организация - создание объекта Topic
            Topic topic = new Topic
            {
                TopicId = 0,
                TopicName = "Bla0",
                ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>",
                Messages = null
            };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(topic);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveTopic(It.IsAny<Topic>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        //Модульное тестирование: удаление темы
        //[TestMethod]
        //public void Can_Delete_Valid_Topics()
        //{
        //    // Организация - создание объекта Topic
        //    Topic topic = new Topic
        //    {
        //        TopicId = 1,
        //        TopicName = "Bla1",
        //        ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>",
        //        Messages = null
        //    };

        //    // Организация - создание имитированного хранилища данных
        //    Mock<ITopicRepository> mock = new Mock<ITopicRepository>();
        //    mock.Setup(m => m.GetAll).Returns(new IDbSet<Topic>
        //    {
        //        new Topic { TopicId = 1,
        //                    TopicName = "Bla1", 
        //                    ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
        //                    Messages = null},
        //        new Topic { TopicId = 2,
        //                    TopicName = "Bla2", 
        //                    ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
        //                    Messages = null},
        //        new Topic { TopicId = 3,
        //                    TopicName = "Bla3", 
        //                    ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
        //                    Messages = null}
        //    });

        //    // Организация - создание контроллера
        //    AdminController controller = new AdminController(null, mock.Object, null);

        //    // Действие - удаление темы
        //    controller.Delete(topic.TopicId);

        //    // Утверждение - проверка того, что метод удаления в хранилище
        //    // вызывается для корректного объекта Topic
        //    mock.Verify(m => m.DeleteTopic(topic.TopicId));
        //}

        private List<Category> Initial_Setting()
        {
            List<Category> result = new List<Category>
            {
                new Category 
                { 
                    CategoryId = 1, 
                    CategoryName = "Томаты", 
                    Topics = new List<Topic>{
                        new Topic
                        {
                            TopicId = 7,
                            TopicName = "Bla7", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        }
                    }
                },
                new Category 
                { 
                    CategoryId = 2,
                    CategoryName = "Селекция томатов", 
                    Topics = new List<Topic>
                    {
                        new Topic
                        {
                            TopicId = 1,
                            TopicName = "Bla1", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        },
                        new Topic
                        {
                            TopicId = 2,
                            TopicName = "Bla2", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        },
                        new Topic
                        {
                            TopicId = 3,
                            TopicName = "Bla3", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        },
                        new Topic
                        {
                            TopicId = 4,
                            TopicName = "Bla4", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        },
                        new Topic
                        {
                            TopicId = 5,
                            TopicName = "Bla5", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        },
                        new Topic
                        {
                            TopicId = 6,
                            TopicName = "Bla6", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        }
                    }
                },
                new Category { 
                    CategoryId = 3, 
                    CategoryName = "Виноград", 
                    Topics = new List<Topic>{
                        new Topic
                        {
                            TopicId = 8,
                            TopicName = "Bla8", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        }
                    }
                },
                new Category { 
                    CategoryId = 4, 
                    CategoryName = "Сад", 
                    Topics = new List<Topic>{
                        new Topic
                        {
                            TopicId = 9,
                            TopicName = "Bla9", 
                            ContentTopic = "<p>Tincidunt integer eu augue augue nunc elit dolor</p>", 
                            Messages = null
                        }
                    }
                }
            };

            return result;
        }
    }
}
