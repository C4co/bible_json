using BibleJson.Services;
using BibleJson.Domain;
using System.Text.Json;
using System.IO.Compression;

namespace App
{
    public enum OutputType
    {
        json,
        gzip
    }

    class Program
    {
        static public async Task Process(
            string url,
            string lang,
            string fileName,
            OutputType outputType = OutputType.gzip
        )
        {
            BibleService bibleService = new(
                new HttpClient(),
                lang
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

            if (outputType == OutputType.json)
            {
                File.WriteAllText($"../books/{fileName}.json", json);
                return;
            }

            if (outputType == OutputType.gzip)
            {
                using var fileStream = File.Create($"../books/{fileName}.gz");
                using var gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
                using var writer = new StreamWriter(gzipStream);
                await writer.WriteAsync(json);
                return;
            }
        }

        public static async Task Main(string[] args)
        {
            //Gzip

            await Process(
                url: "https://www.bibliaonline.com.br/acf/livros",
                lang: "pt-br",
                fileName: "bible_pt-br"
            );

            await Process(
                url: "https://www.bibliaonline.com.br/acv/livros",
                lang: "en",
                fileName: "bible_en"
            );

            await Process(
                url: "https://www.bibliaonline.com.br/bm1844/livros",
                lang: "fr",
                fileName: "bible_fr"
            );

            await Process(
                url: "https://www.bibliaonline.com.br/itadio/livros",
                lang: "it",
                fileName: "bible_it"
            );

            await Process(
                url: "https://www.bibliaonline.com.br/sev/livros",
                lang: "es",
                fileName: "bible_es"
            );

            // Json

            await Process(
                url: "https://www.bibliaonline.com.br/acf/livros",
                lang: "pt-br",
                fileName: "bible_pt-br",
                outputType: OutputType.json
            );

            await Process(
                url: "https://www.bibliaonline.com.br/acv/livros",
                lang: "en",
                fileName: "bible_en",
                outputType: OutputType.json
            );

            await Process(
                url: "https://www.bibliaonline.com.br/bm1844/livros",
                lang: "fr",
                fileName: "bible_fr",
                outputType: OutputType.json
            );

            await Process(
                url: "https://www.bibliaonline.com.br/itadio/livros",
                lang: "it",
                fileName: "bible_it",
                outputType: OutputType.json
            );

            await Process(
                url: "https://www.bibliaonline.com.br/sev/livros",
                lang: "es",
                fileName: "bible_es",
                outputType: OutputType.json
            );
        }
    }
}
