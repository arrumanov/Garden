using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garden.Domain.Abstract;

namespace Garden.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            //коллекция названий ссылок на Category в панели навигации
            IEnumerable<string> categories = new List<string>{
                                                 "Томаты",
                                                 "Селекция томатов",
                                                 "Виноград",
                                                 "Сад"
                                             };
            return PartialView(categories);
        }

    }
}
