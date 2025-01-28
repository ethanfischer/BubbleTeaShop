using System.Collections.Generic;
namespace DefaultNamespace
{
    public class Level4Ingredients : ILevelIngredients
    {
        public Dictionary<int, string> BobaOptions => new()
        {
            { 0, "-" },
            { 1, "Regular Boba" },
            { 2, "Grass Jelly" },
            { 3, "Strawberry Boba" },
            { 4, "Mango Boba" },
            { 5, "Blueberry Boba" },
        };

        public Dictionary<int, string> SugarOptions => new()
        {
            { 0, "-" },
            { 1, "Less Sugar" },
            { 2, "Regular Sugar" },
            { 3, "Extra Sugar" },
        };
        
        public Dictionary<int, string> TeaOptions { get; }

        public Dictionary<int, string> IceOptions => new()
        {
            { 0, "-" },
            { 1, "Less Ice" },
            { 2, "Regular Ice" },
            { 3, "Extra Ice" }
        };

        public Dictionary<int, string> ExtraToppingOptions => new()
        {
            { 0, "-" },
            { 1, "Cheese Foam" }
        };
    }
}
