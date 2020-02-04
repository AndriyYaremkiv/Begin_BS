using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

//namespace Begin_BS.Controllers
namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        public ActionResult Index()     //звичайний синхронний метод
        /*
        //асинхронний метод, повинен повертати об'єтк типу Task     -- !!!
        // IIS - пул потоків 2000-5000
        //+ using System.Threading.Tasks        // need add namespace
        public async Task<ActionResult> BookList()     
        */
        {
            // получаем из бд все объекты Book
            //IEnumerable<Book> books = db.Books;
            var books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            //ViewBag.Books = books;
            // возвращаем представление
            //return View();        //replaced by next line to check new view
            //return View("~/Views/Some/SomeView.cshtml");
            return View(books);      //return values not using ViewBag

        }
        public ActionResult BookIndex()     //звичайний синхронний метод
        {
            var books = db.Books;
            return View(books);     
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return purchase.Person + " , дякуємо за покупку!";
        }
    }
}