using System.Collections.Generic;
namespace DefaultNamespace
{
    public class Level1Ingredients : ILevelIngredients
    {
        public Dictionary<int,string> BobaOptions => new()
        {
            {0, "-"},
            {1,"Regular Boba"}
        };
        
        public Dictionary<int,string> IceOptions => new()
        {
            {0, "-"},
            {1, "Less Ice"},
        };


        public Dictionary<int,string> SugarOptions => new()
        {
            {0, "-"},
            {1, "Less Sugar"},
        };
        public Dictionary<int, string> TeaOptions => new()
        {
            {0, "Regular Tea"},
        };

        public Dictionary<int,string> ExtraToppingOptions => new()
        {
            {0, "-"},
        };
        
    }
}
