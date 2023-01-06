using DapperLearningAPI.Models;
using DapperLearningAPI.Persistence.Commands;

namespace DapperLearningAPI.Helpers
{
    public class CastHelper
    {
        public static TTarget Cast<TSource, TTarget>(TSource model)
            where TSource : BaseModel
            where TTarget : BaseModel
        {
            var result = model as TTarget;
            if (result is null)
            {
                throw new InvalidCastException();
            }

            return result;
        }
    }
}
