using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using LibrarySystem.Models;
using LibrarySystemMvc.Web.Areas.Admin.Models;
using System.Net;
using System.Data.Entity;

namespace LibrarySystemMvc.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        //
        // GET: /Admin/Books/
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(int? page, string sortBy, string search)
        {
            ViewBag.SortByTitle = string.IsNullOrEmpty(sortBy) ? "title desc" : "";
            ViewBag.SortByAuthor = sortBy == "author" ? "author desc" : "author";
            ViewBag.SortByIsbn = sortBy == "isbn" ? "isbn desc" : "isbn";
            ViewBag.SortByUrl = sortBy == "url" ? "url desc" : "url";
            ViewBag.SortByCategory = sortBy == "category" ? "category desc" : "category";

            var books = db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => (b.Title.Contains(search) || b.Author.Contains(search)));
            }

            books = SortCollection(books, sortBy);

            var booksViewModel = books.Select(b => new BookIndexModel
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    Url = b.Url,
                    ISBN = b.ISBN,
                    CategoryName = b.Category.Name
                });

            return View(booksViewModel.ToPagedList(page ?? 1, 5));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookCreateModel book)
        {
            if (ModelState.IsValid)
            {
                Book newBook = new Book
                {
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    Url = book.Url,
                    Description = book.Description,
                    CategoryId = book.Category
                };

                db.Books.Add(newBook);
                db.SaveChanges();
                TempData["Message"] = string.Format("Book \"{0}\" succesfully added", book.Title);
                TempData["bootstrapClass"] = "alert alert-success";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id == book.CategoryId
            }).ToList();
            ViewBag.Categories = categories;

            BookEditModel bookModel = new BookEditModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Url = book.Url,
                Description = book.Description,
                Category = book.CategoryId
            };

            return View(bookModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookEditModel book)
        {
            if (ModelState.IsValid)
            {
                Book updatedBook = new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    Url = book.Url,
                    Description = book.Description,
                    CategoryId = book.Category
                };
                db.Entry(updatedBook).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = string.Format("Book \"{0}\" succesfully updated", book.Title);
                TempData["bootstrapClass"] = "alert alert-success";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(id);
            if (book==null)
            {
                return HttpNotFound();
            }
            BookDeleteModel bookModel = new BookDeleteModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            };
            return View(bookModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var book = db.Books.Find(id);
            db.Entry(book).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["Message"] = string.Format("Book \"{0}\" succesfully deleted", book.Title);
            TempData["bootstrapClass"] = "alert alert-success";
            return RedirectToAction("Index");
        }

        private IQueryable<Book> SortCollection(IQueryable<Book> books,string sortBy)
        {
            switch (sortBy)
            {
                case "title desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "author desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                case "isbn desc":
                    books = books.OrderByDescending(b => b.ISBN);
                    break;
                case "url desc":
                    books = books.OrderByDescending(b => b.Url);
                    break;
                case "category desc":
                    books = books.OrderByDescending(b => b.Category.Name);
                    break;
                case "author":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "isbn":
                    books = books.OrderBy(b => b.ISBN);
                    break;
                case "url":
                    books = books.OrderBy(b => b.Url);
                    break;
                case "category":
                    books = books.OrderBy(b => b.Category.Name);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }
            return books;
        }

	}
}