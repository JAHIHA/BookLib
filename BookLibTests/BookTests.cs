using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Tests
{
    [TestClass()]
    public class BookTests
    {
        private readonly Book _book =new Book() { Id= 1, Title="Mockingbird", Price=1100 };
        private readonly Book _nullTitle = new Book() { Id = 2, Price = 300 };
        private readonly Book _littleTittle = new Book() { Id = 3, Title = "No", Price = 700 };
        private readonly Book _negativePrice = new Book() { Id = 4, Title = "Trainer", Price = -60 };
        private readonly Book _overPrice = new Book() { Id= 5, Title= "Platypus", Price=1300 };
        [TestMethod()]
        public void ToStringTest()
        {
            
            Book book = new Book(1, "test", 500);

           
            string expected = "{ Id=1, Title=test, Price=500 }";

            
            string actual = book.ToString();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ParameterlessConstructorTest()
        {
            var book = new Book();
            Assert.AreEqual(0, book.Id);         
            Assert.AreEqual(null, book.Title);   
            Assert.AreEqual(0, book.Price);
        }

        [TestMethod()]
        public void ParameterizedConstructorTest()
        {
            var book = new Book(1, "Some Title", 500);
            Assert.AreEqual(1, book.Id);           
            Assert.AreEqual("Some Title", book.Title); 
            Assert.AreEqual(500, book.Price);

        }

        [TestMethod()]
        public void ValidateTitleTest()
        {
            _book.ValidateTitle();
            Assert.ThrowsException<ArgumentNullException>(() => _nullTitle.ValidateTitle());
            Assert.ThrowsException<ArgumentException>(() => _littleTittle.ValidateTitle());
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            _book.ValidatePrice();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _negativePrice.ValidatePrice());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _overPrice.ValidatePrice());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            _book.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => _nullTitle.Validate());
            Assert.ThrowsException<ArgumentException>(() => _littleTittle.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _negativePrice.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _overPrice.Validate());
        }
    
    }
}