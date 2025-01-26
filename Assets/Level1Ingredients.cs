using System.Collections.Generic;
namespace DefaultNamespace
{
    public class Level1Ingredients : ILevelIngredients
    {
        public Dictionary<int,string> BobaOptions => new()
        {
            {0, "-"},
            {1,"Boba"}
        };

        public Dictionary<int,string> SugarOptions => new()
        {
            {0, "-"},
            {2, "Regular Sugar"},
        };

        public Dictionary<int,string> IceOptions => new()
        {
            {0, "-"},
            {2, "Regular Ice"},
        };

        public Dictionary<int,string> ExtraToppingOptions => new()
        {
            {0, "-"},
        };
        
    }
}
