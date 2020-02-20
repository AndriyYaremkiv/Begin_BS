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

            SelectList authors = new SelectList(db.Books, "Author", "Name");    //<4.6 Робота з формами>
            ViewBag.Authors = authors;

            ViewBag.Message = "Це часткове представлення";
            return View(books);      //return values not using ViewBag

        }
        //<5.2 Шаблонні хелпери>
        ///*
        public ActionResult GetBook(int id)     //звичайний синхронний метод
        {
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
        }
        //*/
        //<4.6 Робота з формами>
        [HttpPost]
        //public string GetForm(string text)  // для @Html.TextArea 
        //public string GetForm(string color) // для @Html.RadioButton 
        //public string GetForm(bool set)     // для @Html.CheckBox 
        public string GetForm(string author)  // для @Html.DropDownList 
        {
            //return text;              // для @Html.TextArea 
            //return color;             // для @Html.RadioButton
            //return set.ToString();    // для @Html.CheckBox
            return author;              // для @Html.DropDownList
        }
        //<\4.6 Робота з формами>
        public ActionResult BookIndex()     //звичайний синхронний метод
        {
            var books = db.Books;
            return View(books);     
        }
        public ActionResult GetList()     //звичайний синхронний метод
        {
            string[] states = new string[] { "USA", "UK", "Spain", "Italy" };
            return PartialView(states);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            Purchase purchase = new Purchase { BookId = id, Person = "Невідомо" };
            return View(purchase);
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