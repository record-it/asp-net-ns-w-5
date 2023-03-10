namespace wykład_4.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ISet<Book> Books { get; set; }

        public override string? ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}
