using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BookLib.Tests
{
    [TestClass()]
    public class BooksRepositoryTests
    {
        private IBookRepository _repo;
        private readonly Book _badBook = new Book() { Id = 11, Title = "wot", Price = 130000 };

        [TestInitialize]
        public void Init()
        {
            _repo = new BooksRepository();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(_repo.GetById(1));
            Assert.IsNull(_repo.GetById(100));
        }

        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<Book> books = _repo.Get();
            Assert.AreEqual(8, books.Count()); 

            IEnumerable<Book> sortedbooks = _repo.Get(orderBy: "Title_asc");
            Assert.AreEqual("1984", sortedbooks.First().Title);

            IEnumerable<Book> sortedBooks2 = _repo.Get(orderBy: "Price_asc");
            Assert.AreEqual(500, sortedBooks2.First().Price);

            IEnumerable<Book> defaultCaseResult = _repo.Get(orderBy: "InvalidOrderBy");
            
            Assert.IsNotNull(defaultCaseResult);
        }

        [TestMethod()]
        public void GetFilteredTest()
        {
            IEnumerable<Book> booksAbovePrice = _repo.Get(abovePrice: 1002);
            Assert.AreEqual(1, booksAbovePrice.Count());

            IEnumerable<Book> booksBelowPrice = _repo.Get(underPrice: 1003);
            Assert.AreEqual(7, booksBelowPrice.Count()); 
        }

        [TestMethod()]
        public void GetSortedTest()
        {
            IEnumerable<Book> sortedByTitle = _repo.Get(orderBy: "Title_asc");
            Assert.IsTrue(sortedByTitle.SequenceEqual(sortedByTitle.OrderBy(book => book.Title)));

            IEnumerable<Book> sortedByTitleDesc = _repo.Get(orderBy: "Title_desc");
            Assert.IsTrue(sortedByTitleDesc.SequenceEqual(sortedByTitleDesc.OrderByDescending(book => book.Title)));

            IEnumerable<Book> sortedByPrice = _repo.Get(orderBy: "Price_asc");
            Assert.IsTrue(sortedByPrice.SequenceEqual(sortedByPrice.OrderBy(book => book.Price)));

            IEnumerable<Book> sortedByPriceDesc = _repo.Get(orderBy: "Price_desc");
            Assert.IsTrue(sortedByPriceDesc.SequenceEqual(sortedByPriceDesc.OrderByDescending(book => book.Price)));
        }

        [TestMethod()]
        public void AddTest()
        {
            Book b = new() { Id = 12, Title = "Test", Price = 1021 };
            Assert.AreEqual(12, _repo.Add(b).Id);
            Assert.AreEqual(9, _repo.Get().Count()); 

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(_badBook));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.IsNull(_repo.Delete(100));
            Assert.AreEqual(1, _repo.Delete(1)?.Id);
            Assert.AreEqual(7, _repo.Get().Count()); 
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Book b = new() { Id = 10, Title = "Test", Price = 1021 };
            Assert.IsNull(_repo.Update(100, b));
            Assert.AreEqual(1, _repo.Update(1, b)?.Id);
            Assert.AreEqual(8, _repo.Get().Count()); 

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, _badBook));
        }
    }
}