﻿using Repositories;
using Domain;
using System.Text.Json;

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

        foreach (var book in bible.books)
        {
            var numberOfChapters = await catholicBibleRepository.getNumberOfChapters(book);
            book.numberOfChapters = numberOfChapters;

            for (int i = 1; i <= numberOfChapters; i++)
            {
                var chapter = await catholicBibleRepository.GetVerses(book, i);

                Console.WriteLine($"Processing... {book.name} Chapter: {i}");

                book.chapters?.Add(chapter);
            }
        }

        var json = JsonSerializer.Serialize(bible);
        File.WriteAllText("bible_pt-br.json", json);
    }
}