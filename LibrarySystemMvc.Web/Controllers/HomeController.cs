using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using LibrarySystemMvc.Web.ViewModels;

namespace LibrarySystemMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        public ActionResult Index()
        {
            var categories = db.Categories
                .Select(cat => new CategoryBooksViewModel
                {
                    CategoryName = cat.Name,
                    Books = cat.Books.Select(book => new BookTitleAuthorViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author
                    })
                }).ToList();
            return View(categories);
        }

        public ActionResult BookDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var book = db.Books.Where(b => b.Id == id.Value).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }

            BookDetailsViewModel viewModel = new BookDetailsViewModel
            {
                Author = book.Author,
                Description = book.Description,
                Id = book.Id,
                Title = book.Title,
                Url = book.Url,
                ISBN = book.ISBN
            };
            return View(viewModel);
        }

        public ActionResult Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var books = db.Books
                    .Where(b => (b.Title.Contains(search) || b.Author.Contains(search)))
                    .OrderBy(b => b.Title)
                    .Select(b => new BookSearchViewModel
                    {
                        Id = b.Id,
                        Author = b.Author,
                        Title = b.Title,
                        Category = b.Category.Name
                    });
                return View(books);
            }
            else
            {
                var books = db.Books
                    .OrderBy(b => b.Title)
                    .Select(b => new BookSearchViewModel
                    {
                        Id = b.Id,
                        Author = b.Author,
                        Title = b.Title,
                        Category = b.Category.Name
                    });
                return View(books);
            }
        }
    }
}