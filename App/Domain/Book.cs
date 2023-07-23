namespace Domain
{
    class Book
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Abbrev { get; set; }
        public int Testament { get; set; }
        public List<List<string>>? Chapters { get; set; }

        public Book(
            string name,
            string link,
            string abbrev,
            List<List<string>>? chapters
        )
        {
            Name = name;
            Link = link;
            Abbrev = abbrev;
            Chapters = chapters;
        }
    }
};

