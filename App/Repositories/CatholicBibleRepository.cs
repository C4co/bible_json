using Domain;

using HtmlAgilityPack;

namespace Repositories
{
    public enum BookType
    {
        OldTestament = 0,
        NewTestament = 1
    }

    class CatholicBibleRepository
    {
        private readonly HttpClient _httpClient;

        public CatholicBibleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //get books
        public async Task<List<Book>> GetBooks(BookType bookType)
        {
            try
            {
                List<Book> list = new List<Book>();

                var response = await _httpClient.GetAsync("https://www.bibliaonline.com.br/vc/index");

                var reponseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(reponseBody);

                var uls = htmlDoc.DocumentNode.SelectNodes("//ul");

                var oldTestament = uls?[((int)bookType)];

                foreach (var li in oldTestament!.ChildNodes)
                {
                    var link = li.ChildNodes[0];

                    var bookName = link.InnerText;

                    var bookLink = link.Attributes["href"].Value;

                    var abbrev = bookLink.Substring(bookLink.Length - 3).Replace("/", "");

                    Book book = new Book(
                        name: bookName,
                        link: bookLink,
                        abbrev: abbrev,
                        chapters: null
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
    }

}
