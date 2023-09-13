using System.Text.Json.Serialization;

namespace BibleJson.Domain
{
    public class Bible
    {
        [JsonPropertyName("books")]
        public List<Book> Books { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        public Bible(List<Book> books, string lang)
        {
            Books = books;
            Lang = lang;
        }
    }
}