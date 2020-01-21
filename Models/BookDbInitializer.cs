using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookStore.Models
{
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Кобзар", Author = "Т.Шевченко", Price = 220 });
            db.Books.Add(new Book { Name = "Каменяр", Author = "І.Франко", Price = 180 });
            db.Books.Add(new Book { Name = "Лісова пісня", Author = "Л.Українка", Price = 190 });

            base.Seed(db);
        }
    }
}