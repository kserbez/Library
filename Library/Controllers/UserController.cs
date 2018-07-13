using Library.DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private UsersDA usersDA = new UsersDA();

        [HttpGet]
        public ActionResult Index()
        {
            List<UserModel> allUsers = usersDA.GetAllUsersAsList();
            return View(allUsers);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {

            return View();
        }

    }
}