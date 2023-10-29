using BibleJson.Domain;

using HtmlAgilityPack;

namespace BibleJson.Services
{
    public enum BookType
    {
        OldTestament = 0,
        NewTestament = 1
    }

    class BibleService
    {
        private readonly HttpClient _httpClient;
        public string Lang;

        public BibleService(HttpClient httpClient, string lang)
        {
            _httpClient = httpClient;
            Lang = lang;
        }

        public async Task<List<Book>> GetBooks(BookType bookType, string url)
        {
            try
            {
                List<Book> list = new();

                var response = await _httpClient.GetAsync(url);

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new();

                htmlDoc.LoadHtml(reponseBody);

                var uls = htmlDoc.DocumentNode.SelectNodes("//ul[@class='css-unklqn']");

                var testament = uls?[(int)bookType];

                foreach (var li in testament!.ChildNodes)
                {
                    var link = li.SelectSingleNode(".//a");

                    var bookName = link.InnerText ?? "null";

                    var bookLink = link.Attributes["href"].Value ?? "null";

                    var abbrev = bookLink[^3..].Replace("/", "");

                    if (Lang == "en") bookName = BibleTranslate.TranslateToEnglish(bookName);
                    if (Lang == "fr") bookName = BibleTranslate.TranslateToFrench(bookName);
                    if (Lang == "es") bookName = BibleTranslate.TranslateToSpanish(bookName);
                    if (Lang == "it") bookName = BibleTranslate.TranslateToItalian(bookName);

                    Book book = new(
                        name: bookName,
                        link: bookLink,
                        abbrev: abbrev,
                        chapters: new List<List<string>>()
                    );

                    list.Add(book);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetNumberOfChapters(Book book)
        {
            try
            {
                var response = await _httpClient.GetAsync(book.Link + "/1");

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new();

                htmlDoc.LoadHtml(reponseBody);

                var ul = htmlDoc.DocumentNode.SelectNodes("//aside/ul");

                var numberOfChapters = ul![0].ChildNodes.Count;

                return numberOfChapters;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<string>> GetVerses(Book book, int chapter)
        {
            try
            {
                List<string> list = new();

                var response = await _httpClient.GetAsync($"{book.Link}/{chapter}");

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new();

                htmlDoc.LoadHtml(reponseBody);

                var article = htmlDoc.DocumentNode.SelectNodes("//article");

                var spans = article![0].SelectNodes(".//span[@class='t']");

                foreach (var span in spans!)
                {
                    var verse = span.InnerText;

                    list.Add(verse);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
