using Repositories;
using Domain;

class Program
{
    static public async Task Main(string[] args)
    {
        CatholicBibleRepository catholicBibleRepository = new CatholicBibleRepository(
            new HttpClient()
        );

        List<Book> oldTestamentBooks = await catholicBibleRepository.GetBooks(BookType.OldTestament);
        List<Book> newTestamentBooks = await catholicBibleRepository.GetBooks(BookType.NewTestament);

        Bible bible = new Bible(oldTestamentBooks.Concat(newTestamentBooks).ToList());

        Console.WriteLine($"Books count: {bible.books.Count}");
    }
}