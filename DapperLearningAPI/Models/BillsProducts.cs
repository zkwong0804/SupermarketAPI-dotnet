using DapperLearningAPI.Helpers;

namespace DapperLearningAPI.Models
{
    public class BillsProducts : BaseModel
    {
        public int BillID { get; set; }
        public int ProductID { get; set; }

        public override bool Equals(object? obj)
        {
            var bp = obj as BillsProducts;
            if (bp is null)
            {
                throw new InvalidCastException();
            }

            return this.ProductID.Equals(bp.ProductID);
        }

        public override int GetHashCode()
        {
            return BillID.GetHashCode();
        }
    }
}
