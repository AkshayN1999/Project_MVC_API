using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PROJECT_MVC_API;

namespace PROJECT_MVC_API.Controllers
{
    public class MVCController : Controller
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: MVC
        public ActionResult Index()
        {
            IEnumerable<BookTab> books = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                var responsetask = client.GetAsync("getwebapitabs");
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<IList<BookTab>>();
                    readtask.Wait();
                    books = readtask.Result;
                }
                else
                {
                    books = Enumerable.Empty<BookTab>();
                }
            }
            return View(books);
            //return View(db.BookTabs.ToList());
        }

        // GET: MVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTab bookTab = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                var responsetask = client.GetAsync($"getwebapitab/{id}");
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<BookTab>();
                    readtask.Wait();
                    bookTab = readtask.Result;
                }
                else
                {
                    bookTab = new BookTab();
                }
            }
            return View(bookTab);
            //BookTab bookTab = db.BookTabs.Find(id);
            //if (bookTab == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(bookTab);
        }

        // GET: MVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookName,BookSummary")] BookTab bookTab)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                    var posttask = client.PostAsJsonAsync<BookTab>("postwebapitab", bookTab);
                    posttask.Wait();

                    var result = posttask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                //db.BookTabs.Add(bookTab);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                return View(bookTab);
            }
            return View(bookTab);
        }

        // GET: MVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTab employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                var responseTask = client.GetAsync($"getwebapitab/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookTab>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new BookTab();
                }
            }
            return View(employee);
            //BookTab bookTab = db.BookTabs.Find(id);
            //if (bookTab == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(bookTab);
        }

        // POST: MVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookName,BookSummary")] BookTab bookTab)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                    var postTask = client.PutAsJsonAsync<BookTab>($"putwebapitab/{bookTab.BookId}", bookTab);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    //db.Entry(bookTab).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
            }
            return View(bookTab);
        }

        // GET: MVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTab employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                var responseTask = client.GetAsync($"getwebapitab/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookTab>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new BookTab();
                }
            }
            return View(employee);
            //BookTab bookTab = db.BookTabs.Find(id);
            //if (bookTab == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(bookTab);
        }

        // POST: MVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57391/api/API/");
                var postTask = client.DeleteAsync($"deletewebapitab/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Delete");
            //BookTab bookTab = db.BookTabs.Find(id);
            //db.BookTabs.Remove(bookTab);
            //db.SaveChanges();
            //return RedirectToAction("Index");
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
