using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public interface IBookRepository
    {
        public Book GetById(int id);

        public IEnumerable<Book> Get(int? abovePrice = null, int? underPrice = null, string? orderBy = null);

        public Book Add(Book book);

        public Book Delete(int id);

        public Book Update(int id, Book book);
    }
}
