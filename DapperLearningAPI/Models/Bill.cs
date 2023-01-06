namespace DapperLearningAPI.Models
{
    public class Bill : ModelWithID
    {
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; } = new ();
    }
}
