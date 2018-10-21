using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    Description = "Use Knockout.js to design and build dynamic client-side web applications that are extremely responsive and easy to maintain. This example-driven book shows you how to use this lightweight JavaScript framework and its Model-View-ViewModel (MVVM) pattern. You’ll learn how to build your own data bindings, extend the framework with reusable functions, and work with a server to enhance your client-side application with persistence. In the final chapter, you’ll build a shopping cart to see how everything fits together.",
                    ImageUrl = "http://ecx.images-amazon.com/images/I/51T%2BWt430bL._AA160_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "Knockout.js: Building Dynamic Client-Side Web Applications"
                },
                new Book()
                {
                    Author = author,
                    Description = "Gain hands-on experience with the amazing PhoneGap library, using the practical recipes in this handy guide. With these solutions, you can enable your mobile web apps to interact with device-specific features such as the accelerometer, GPS, camera, and address book. Learn how to use your knowledge of HTML, CSS, and JavaScript to build full mobile apps for iOS, Android, and several other platforms without rewriting apps in the native platform language. Each recipe includes sample code you can use in your project right away, as well as a discussion of why the solution works.",
                    ImageUrl = "https://covers.oreillystatic.com/images/0636920023708/cat.gif",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "20 Recipes for Programming PhoneGap: Cross-Platform Mobile Development"
                },
                new Book()
                {
                    Author = author,
                    Description = "There's no need to reinvent the wheel every time you run into a problem with ASP.NET's Model-View-Controller (MVC) framework. This concise cookbook provides recipes to help you solve tasks many web developers encounter every day. Each recipe includes the C# code you need, along with a complete working example of how to implement the solution. Learn practical techniques for applying user authentication, providing faster page reloads, validating user data, filtering search results, and many other issues related to MVC3 development.",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51SEAOaYfKL._SX379_BO1,204,203,200_.jpg",
                    Isbn = "1491914319",
                    Synopsis = " ",
                    Title = "20 Recipes for Programming MVC 3: Faster, Smarter Web Development"
                },
                new Book()
                {
                    Author = author,
                    Description = "This book was written by a web developer for web developers. It is written in a fashion where it will be useful for beginning web developers and also help advanced web developers learn more. To ensure you are not completely lost, knowledge of PHP, Javascript, HTML, and CSS are required. A basic understanding of Object Oriented Programming (OOP) is useful, but not 100 percent required. Whether you are an advanced web developer or a beginner looking to learn CakePHP, this book promises to help you build simple and complex web sites, quickly and easily. The goal of this book is to provide a wide variety of useful functionality that is required by most websites today. In the final chapter, we will take all that we’ve learned and create one final large project. During this process, I will instill my knowledge in taking a project from start to finish including creating a scope of work to ensure we have accomplished our target goals. By the end of this book, you should be able to build fully functional websites using CakePHP. Hopefully, you should also be able to cut your development time at least in half, if not more!",
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41iVfKTw4zL._SX322_BO1,204,203,200_.jpg",
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
        [JsonProperty()]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Biography { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}