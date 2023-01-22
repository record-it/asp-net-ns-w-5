namespace wykład_4.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }

        public ISet<Book> Books { get; set; }
    }
}
