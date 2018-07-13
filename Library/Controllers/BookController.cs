using Library.DataAccess;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    //[Authorize]
    public class BookController : Controller
    {
        private BooksDA booksDA = new BooksDA();


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<BookModel> allBooks = booksDA.GetAllBooksAsList();
            return View(allBooks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new BookModel());
        }

        // POST: /Books/Create
        [HttpPost]
        public ActionResult Create(BookModel bookModel)
        {
            booksDA.InsertBook(bookModel);
            return RedirectToAction("Index");
        }

        // GET: /Books/ChangeQuantity/5
        public ActionResult ChangeQuantity(int? id)
        {
            if (id == null)
                return HttpNotFound();

            BookModel bookModel = booksDA.GetBookModelBy((int)id);

            if (bookModel == null) // that is doesn't exist
                return HttpNotFound();

            return View(bookModel);
        }

        // POST: /Books/ChangeQuantity/5
        [HttpPost]
        public ActionResult ChangeQuantity(BookModel bookModel)
        {
            if (!ModelState.IsValid)
                return View(bookModel.Id);

            var tmpBook = booksDA.GetBookModelBy(bookModel.Id);

            if (tmpBook != null)
            {
                tmpBook.Quantity = bookModel.Quantity;
                booksDA.UpdateBook(tmpBook);
            }

            return RedirectToAction("Index");
        }

        // GET: /Books/Take/5
        public ActionResult Take(int? id)
        {
            if (id == null)
                return HttpNotFound();

            BookModel bookModel = booksDA.GetBookModelBy((int)id);

            if (bookModel == null) // that is doesn't exist
                return RedirectToAction("Index");

            return View(bookModel);
        }

        // POST: /Books/Take/5
        [HttpPost]
        public ActionResult Take(BookModel bookModel)
        {
            // TODO: implement take of a book
            return View(bookModel);
        }


        // GET: /Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            booksDA.DeleteBook((int)id);
            return RedirectToAction("Index");
        }

    }
}