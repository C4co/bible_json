namespace Domain
{
    class Book
    {
        public string name { get; set; }
        public string link { get; set; }
        public string abbrev { get; set; }
        public int testament { get; set; }
        public int? numberOfChapters { get; set; }
        public List<List<string>>? chapters { get; set; }

        public Book(
            string name,
            string link,
            string abbrev,
            List<List<string>>? chapters,
            int? numberOfChapters = null
        )
        {
            this.name = name;
            this.link = link;
            this.abbrev = abbrev;
            this.chapters = chapters;
            this.numberOfChapters = numberOfChapters;
        }
    }
};

