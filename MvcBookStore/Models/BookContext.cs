using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using MvcBookStore.Utils;
using Newtonsoft.Json;

namespace MvcBookStore.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookContext")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new BookInitializer());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class BookInitializer : DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            base.Seed(context);

            var author = new Author()
            {
                Biography = " ",
                FirstName = "Jamie",
                LastName = "Munro"
            };

            var books = new List<Book>()
            {
                new Book()
                {
                    Author = author,
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "Knockout.js: Building Dynamic Client-Side Web Applications"
                },
                new Book()
                {
                    Author = author,
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "20 Recipes for Programming PhoneGap: Cross-Platform Mobile Development"
                },
                new Book()
                {
                    Author = author,
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "20 Recipes for Programming MVC 3: Faster, Smarter Web Development"
                },
                new Book()
                {
                    Author = author,
                    Description = "...",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "Rapid Application Development With CakePHP"
                },
            };

            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Synopsis { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual Author Author { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }

}