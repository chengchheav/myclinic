using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Models;
using MyClinic.Core;

namespace MyClinic.Controllers
{
    public class ExampleController : Controller
    {
        IUserRepository userRepository = new UserRepository();
        public ActionResult Index(int? page)
        {
            //HomeModels homeModels = new HomeModels();
            var users = userRepository.Get();
            //homeModels.users = users;

            //var dummyItems = Enumerable.Range(1, 39).Select(x => "Item " + x);/*don't understad why it loop*/
            var dummyItems = Enumerable.Range(1, users.Count()).Select(x => users);
            var pager = new Pager(dummyItems.Count(), page);

            var exampleModel = new PaginationModels
             {
                 users = users.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                 Pager = pager
             };

            return View(exampleModel);
        }

    }
}
