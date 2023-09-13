using System.Text.Json.Serialization;

namespace BibleJson.Domain
{
    public class Book
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("abbrev")]
        public string Abbrev { get; set; }

        [JsonPropertyName("testament")]
        public int Testament { get; set; }

        [JsonPropertyName("numberOfChapters")]
        public int? NumberOfChapters { get; set; }

        [JsonPropertyName("chapters")]
        public List<List<string>>? Chapters { get; set; }

        public Book(
            string name,
            string link,
            string abbrev,
            List<List<string>>? chapters,
            int? numberOfChapters = null
        )
        {
            Name = name;
            Link = link;
            Abbrev = abbrev;
            Chapters = chapters;
            NumberOfChapters = numberOfChapters;
        }
    }
};

