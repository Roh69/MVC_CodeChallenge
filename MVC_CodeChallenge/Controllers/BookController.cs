using MVC_CodeChallenge.DataAccessLayer;
using MVC_CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CodeChallenge.Controllers
{
    public class BookController : Controller
    {
        BookDAL dl;

        public BookController()
        {
            dl = new BookDAL();
        }

        // GET: Book
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookModel book)
        {
            bool flag = dl.AddBook(book);
            if(flag==true)
            {
                ViewBag.success = "Data Inserted Successfully!!";
            }
            else
            {
                ViewBag.success = "Data not Inserted!!";
            }
            return View();
        }

    

      public ActionResult List()
        {
            var list = dl.GetBooks();
            return View(list);
        }

        public ActionResult Details(int id)
        {
            var result = dl.GetBook(id);
            return View(result);
        }

      
        public ActionResult Edit(int id)
        {
            var result = dl.GetBook(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(BookModel book)
        {
            bool b = dl.UpdateBooks(book.bookId, book);
            if (b == true)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            dl.DeleteBooks(id);
            return RedirectToAction("List");
        }

    }
}