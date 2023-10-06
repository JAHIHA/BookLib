using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLib
{
    public class BooksRepository : IBookRepository { 

    private readonly List<Book> books = new List<Book>
    {
        new Book(1, "The Little Prince", 1000),
        new Book(2, "1984", 500),
        new Book(3, "The Brothers Karamazov", 880),
        new Book(4, "The Hobbit", 937),
        new Book(5, "To Kill a Mockingbird", 960),
        new Book(6, "Titanic", 1003),
        new Book(7, "Dune", 1002),
        new Book(8, "Miracle", 800)
    };

    public Book GetById(int id)
    {
        return books.FirstOrDefault(book => book.Id == id);
    }

    public IEnumerable<Book> Get(int? abovePrice = null, int? underPrice = null, string? orderBy = null)
    {
        IEnumerable<Book> result = new List<Book>(books);

        if (abovePrice != null)
        {
            result = result.Where(book => book.Price > abovePrice);
        }

        if (underPrice != null)
        {
            result = result.Where(book => book.Price < underPrice);
        }

        if (orderBy != null)
        {
            orderBy = orderBy.ToLower();
            switch (orderBy)
            {
                case "title":
                case "title_asc":
                    result = result.OrderBy(book => book.Title);
                    break;
                case "title_desc":
                    result = result.OrderByDescending(book => book.Title);
                    break;
                case "price":
                case "price_asc":
                    result = result.OrderBy(book => book.Price);
                    break;
                case "price_desc":
                    result = result.OrderByDescending(book => book.Price);
                    break;
                default:
                    break;
            }
        }

        return result;
    }

    public Book? Add(Book book)
    {
        book.Validate();
        books.Add(book);
        return book;
    }

    public Book Delete(int id)
    {
        Book book = GetById(id);
        if (book == null)
        {
            return null;
        }
        books.Remove(book);
        return book;
    }

    public Book Update(int id, Book book)
    {
        book.Validate();
        Book existingbook = GetById(id);
        if (existingbook == null)
        {
            return null;
        }
        existingbook.Title = book.Title;
        existingbook.Price = book.Price;
        return existingbook;
    }
}
}
