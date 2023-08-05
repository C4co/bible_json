using Repositories;
using Domain;
using System.Text.Json;

class Program
{
    static public async Task Main()
    {
        BibleRepository catholicBibleRepository = new(
            new HttpClient()
        );

        List<Book> oldTestamentBooks = await catholicBibleRepository.GetBooks(
            BookType.OldTestament,
            "https://www.bibliaonline.com.br/vc/livros"
        );
        List<Book> newTestamentBooks = await catholicBibleRepository.GetBooks(
            BookType.NewTestament,
            "https://www.bibliaonline.com.br/vc/livros"
        );

        Bible bible = new(oldTestamentBooks.Concat(newTestamentBooks).ToList());

        foreach (var book in bible.Books)
        {
            var numberOfChapters = await catholicBibleRepository.GetNumberOfChapters(book);
            book.NumberOfChapters = numberOfChapters;

            for (int i = 1; i <= numberOfChapters; i++)
            {
                var chapter = await catholicBibleRepository.GetVerses(book, i);

                Console.WriteLine($"Processing... {book.Name} Chapter: {i}");

                book.Chapters?.Add(chapter);
            }
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(bible, options);

        File.WriteAllText("bible_pt-br.json", json);
    }
}