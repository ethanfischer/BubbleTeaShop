using System.Collections.Generic;
namespace DefaultNamespace
{
    public interface ILevelIngredients
    {
        Dictionary<int, string> BobaOptions { get; }
        Dictionary<int, string> IceOptions { get; }
        Dictionary<int, string> SugarOptions { get; }
        Dictionary<int, string> ExtraToppingOptions { get; }
    }
}
