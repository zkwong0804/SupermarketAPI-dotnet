namespace DapperLearningAPI.Models
{
    public abstract class ModelWithName : ModelWithID
    {
        public string Name { get; set; } = string.Empty;
    }
}
