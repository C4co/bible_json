namespace Domain
{
    class Bible
    {
        public List<Book> Books { get; set; }

        public Bible(List<Book> books)
        {
            this.Books = books;
        }
    }
}