namespace BookLib

{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

        public Book()
        {

        }
        public Book(int id, string title, int price)
        {
            Id = id;
            Title = title;
            Price = price;
        }
        public override string ToString()
        {
            return $"{{ {nameof(Id)}={Id.ToString()}, {nameof(Title)}={Title}, {nameof(Price)}={Price.ToString()} }}";
        }
        public void ValidateTitle()
        {
            if (Title == null)
            {
                throw new ArgumentNullException(nameof(Title), "Title cannot be null");
            }
            if (Title.Length < 3)
            {
                throw new ArgumentException("Title must be more than two letters: ", nameof(Title));
            }
        }

        public void ValidatePrice()
        {
            if( Price< 0)
            {
                throw new ArgumentOutOfRangeException("Price must not be negative: ", nameof(Price));
            }
            if (Price >1200)
            {
                throw new ArgumentOutOfRangeException("Price must be maximum 1200: ", nameof(Price));
            }
        }

        public void Validate()
        {
            ValidateTitle();
            ValidatePrice();
        }
    }
}