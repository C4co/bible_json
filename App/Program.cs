using BibleJson.Services;
using BibleJson.Domain;
using System.Text.Json;

class Program
{
    static public async Task Process(
        string url,
        string lang,
        string fileName
    )
    {
        BibleService bibleService = new(
            new HttpClient()
        );

        List<Book> oldTestamentBooks = await bibleService.GetBooks(
            BookType.OldTestament,
            url
        );

        List<Book> newTestamentBooks = await bibleService.GetBooks(
            BookType.NewTestament,
            url
        );

        Bible bible = new(
            books: oldTestamentBooks.Concat(newTestamentBooks).ToList(),
            lang: lang
        );

        foreach (var book in bible.Books)
        {
            var numberOfChapters = await bibleService.GetNumberOfChapters(book);
            book.NumberOfChapters = numberOfChapters;

            for (int i = 1; i <= numberOfChapters; i++)
            {
                var chapter = await bibleService.GetVerses(book, i);

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

        File.WriteAllText($"../books/{fileName}", json);
    }

    public static async Task Main(string[] args)
    {
        await Process(
            url: "https://www.bibliaonline.com.br/vc/livros",
            lang: "pt-br",
            fileName: "bible_pt-br.json"
        );

        await Process(
            url: "https://www.bibliaonline.com.br/acv/livros",
            lang: "en",
            fileName: "bible_en.json"
        );
    }
}