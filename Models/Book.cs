namespace wykład_4.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        public string Title{ get; set; }

        public int EditionYear { get; set; }

        public DateTime Created { get; set; }

        public BookDetails BookDetails { get; set; }

        public Publisher? Publisher {  get; set; }

        public ISet<Author> Authors { get; set; }
    }
}
