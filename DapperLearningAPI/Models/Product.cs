namespace DapperLearningAPI.Models
{
    public class Product : ModelWithName
    {
        // use minus one to indicate no value
        public decimal Price { get; set; } = decimal.MinusOne;
        public int CategoryID { get; set; }
    }
}
