namespace Domain
{
    class Bible
    {
        public List<Book> books { get; set; }

        public Bible(List<Book> books)
        {
            this.books = books;
        }
    }
}