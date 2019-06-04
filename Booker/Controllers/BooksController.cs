using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booker.Models;
using Booker.ViewModels;
using System.Data.Entity;

namespace Booker.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var books = _context.Books.ToList();

            return View(books);
        }

        public ActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Book = new Book()
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel
                {
                    Book = new Book()
                };

                return View("BookForm", viewModel);
            }

            if (book.Id == 0)
            {
                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.Single(c => c.Id == book.Id);

                bookInDb.Title = book.Title;
                bookInDb.Author = book.Author;
                bookInDb.YearOfPublication = book.YearOfPublication;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.NumberRented = book.NumberRented;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            if (id > 0)
            {
                var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);

                _context.Books.Remove(bookInDb);
                _context.SaveChanges();
            }


            return RedirectToAction("Index", "Books");

        }


        public ActionResult Details(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }


        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book == null)
                return HttpNotFound();

            var viewModel = new BookFormViewModel
            {
                Book = book
            };

            return View("BookForm", viewModel);
        }

    }
}