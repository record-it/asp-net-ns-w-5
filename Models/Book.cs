namespace wykład_4.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        public string Title{ get; set; }

        public int EditionYear { get; set; }

        public DateTime Created { get; set; }
    }
}
