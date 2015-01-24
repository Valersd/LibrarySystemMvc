using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using LibrarySystemMvc.Web.Areas.Admin.Models;
using PagedList;
using PagedList.Mvc;

namespace LibrarySystemMvc.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: /Admin/Categories/
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(int? page)
        {
            return View(db.Categories.ToList().ToPagedList(page ?? 1, 5));
        }


        // GET: /Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryAddModel category)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category
                {
                    Name = category.Name
                };
                db.Categories.Add(newCategory);
                try
                {
                    db.SaveChanges();
                    TempData["Message"] = string.Format("Category \"{0}\" succesfully added", category.Name);
                    TempData["bootstrapClass"] = "alert alert-success";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Category with that name already exist");
                    return View(category);
                }
            }

            return View(category);
        }

        // GET: /Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            CategoryEditModel categoryModel = new CategoryEditModel
            {
                Name = category.Name
            };

            return View(categoryModel);
        }

        // POST: /Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditModel category)
        {
            Category newCategory = new Category
            {
                Name = category.Name
            };
            if (ModelState.IsValid)
            {
                db.Entry(newCategory).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    TempData["Message"] = string.Format("Category \"{0}\" succesfully updated", newCategory.Name);
                    TempData["bootstrapClass"] = "alert alert-success";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Category with that name already exist");
                    return View(category);
                }
            }
            return View(category);
        }

        // GET: /Admin/Categories/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // POST: /Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Message"] = string.Format("Category \"{0}\" succesfully deleted", category.Name);
            TempData["bootstrapClass"] = "alert alert-success";
            return RedirectToAction("Index", new { page = page });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
