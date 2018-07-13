using Library.DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorsDA authorsDA = new AuthorsDA();

        public ActionResult Index()
        {
            List<AuthorModel> allUsers = authorsDA.GetAllAuthorsAsList();
            return View(allUsers);
        }
    }
}