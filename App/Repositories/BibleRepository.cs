using System.Net;

using Domain;

using HtmlAgilityPack;

namespace Repositories
{
    public enum BookType
    {
        OldTestament = 0,
        NewTestament = 1
    }

    class BibleRepository
    {
        private readonly HttpClient _httpClient;

        public BibleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get books
        public async Task<List<Book>> GetBooks(BookType bookType, string url)
        {
            try
            {
                List<Book> list = new List<Book>();

                var response = await _httpClient.GetAsync(url);

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(reponseBody);

                var uls = htmlDoc.DocumentNode.SelectNodes("//ul[@class='css-1091dtb']");

                var testament = uls?[((int)bookType)];

                foreach (var li in testament!.ChildNodes)
                {

                    var link = li.SelectSingleNode(".//a");

                    var bookName = link.InnerText ?? "null";

                    var bookLink = link.Attributes["href"].Value ?? "null";

                    var abbrev = bookLink.Substring(bookLink.Length - 3).Replace("/", "");

                    Book book = new Book(
                        name: bookName,
                        link: bookLink,
                        abbrev: abbrev,
                        chapters: new List<List<string>>()
                    );

                    list.Add(book);
                }

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> getNumberOfChapters(Book book)
        {
            try
            {
                var response = await _httpClient.GetAsync(book.link + "/1");

                //response body as utf8 string
                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(reponseBody);

                var ul = htmlDoc.DocumentNode.SelectNodes("//aside/ul");

                var numberOfChapters = ul![0].ChildNodes.Count;

                return numberOfChapters;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //get verses
        public async Task<List<string>> GetVerses(Book book, int chapter)
        {
            try
            {
                List<String> list = new List<String>();

                var response = await _httpClient.GetAsync($"{book.link}/{chapter}");

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(reponseBody);

                var article = htmlDoc.DocumentNode.SelectNodes("//article");

                //select all span with class 't' from article
                var spans = article![0].SelectNodes(".//span[@class='t']");

                foreach (var span in spans!)
                {
                    var verse = span.InnerText;
                    var decodedVerse = WebUtility.HtmlDecode(verse);

                    list.Add(decodedVerse);
                }

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }

}
