using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Garden.Domain.Abstract;
using Garden.Domain.Entities;
using Garden.Domain.Concrete;
using Garden.WebUI.Controllers;

using Garden.WebUI.Models;
using Garden.WebUI.HtmlHelpers;

namespace Garden.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //Модульное тестирование средства разбиения на страницы
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());
            CategoryController controller = new CategoryController(mock.Object, null, null);
            controller.pageSize = 3;

            // Действие (act)
            //получить данные, возвращаемые методом контроллера. Мы обращаемся к
            //свойству Model объекта результата, чтобы получить последовательность
            //IEnumerable<Topic>, сгенерированную методом Index(). Затем выполняется 
            //проверка, являются ли эти данные ожидаемыми. В этом случае мы преобразовали
            //последовательность в коллекцию с помощью LINQ-метода ToList() и проверили 
            //длину и значения отдельных объектов
            TopicIndexViewModel result = (TopicIndexViewModel)((ViewResult)controller.Index(null, 2)).Model;

            // Утверждение (assert)
            List<Topic> topics = result.Topics.ToList();
            Assert.IsTrue(topics.Count == 3);
            Assert.AreEqual(topics[0].TopicName, "Bla4");
            Assert.AreEqual(topics[1].TopicName, "Bla5");
            Assert.AreEqual(topics[2].TopicName, "Bla6");
        }

        //Модульное тестирование вспомогательного метода PageLinks()
        //вызываем метод с тестовыми данными и сравниваем результаты с ожидаемой HTML-разметкой
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        //Модульное тестирование разбиения на страницы
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)

            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());
            CategoryController controller = new CategoryController(mock.Object, null, null);

            controller.pageSize = 3;

            // Act
            TopicIndexViewModel result
                = (TopicIndexViewModel)((ViewResult)controller.Index(null, 2)).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 6);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        //Модульное тестирование: фильтрация по категории
        [TestMethod]
        public void Can_Filter_Categories()
        {
            // Организация (arrange)

            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());
            CategoryController controller = new CategoryController(mock.Object, null, null);
            controller.pageSize = 3;

            // Action
            List<Topic> result = ((TopicIndexViewModel)((ViewResult)controller.Index("Селекция томатов", 1)).Model)
                .Topics.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 3);

            //как проинициализировать Topic.Category,  чтобы можно было добраться до Topic.Category.CategoryName
            Assert.IsTrue(result[0].TopicName == "Bla1");
            Assert.IsTrue(result[1].TopicName == "Bla2");
            Assert.IsTrue(result[2].TopicName == "Bla3");
            //Assert.IsTrue(result[3].TopicName == "Bla4" && result[3].Category.CategoryName == "Селекция томатов");
            //Assert.IsTrue(result[4].TopicName == "Bla5" && result[4].Category.CategoryName == "Селекция томатов");
            //Assert.IsTrue(result[5].TopicName == "Bla6" && result[5].Category.CategoryName == "Селекция томатов");
        }

        //Модульное тестирование: счетчик тем определенной категории
        [TestMethod]
        public void Generate_Category_Specific_Category_Count()
        {
            /// Организация (arrange)
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            mock.Setup(m => m.GetAll).Returns(Initial_Setting());
            CategoryController controller = new CategoryController(mock.Object, null, null);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((TopicIndexViewModel)((ViewResult)controller.Index("Томаты")).Model).PagingInfo.TotalItems;
            int res2 = ((TopicIndexViewModel)((ViewResult)controller.Index("Селекция томатов")).Model).PagingInfo.TotalItems;
            int res3 = ((TopicIndexViewModel)((ViewResult)controller.Index("Виноград")).Model).PagingInfo.TotalItems;
            int resAll = ((TopicIndexViewModel)((ViewResult)controller.Index("Сад")).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 0);
            Assert.AreEqual(res2, 6);
            Assert.AreEqual(res3, 0);
            Assert.AreEqual(resAll, 0);
        }

        private List<Category> Initial_Setting()
        {
            List<Category> result = new List<Category>
            {
                new Category 
                { 
                    CategoryId = 1, 
                    CategoryName = "Томаты", 
                    Topics = new List<Topic>()
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
                    Topics = new List<Topic>()
                },
                new Category { 
                    CategoryId = 4, 
                    CategoryName = "Сад", 
                    Topics = new List<Topic>()
                }
            };

            return result;
        }

    }
}