using System.Linq;
using UnityEngine;
namespace DefaultNamespace
{
    public static class OrderFactory
    {
        private static ILevelIngredients _ingredients;
        public static Order CreateOrder()
        {
            _ingredients = GetIngredients();
            return new Order(
                _ingredients.BobaOptions.ElementAt(Random.Range(0, _ingredients.BobaOptions.Count)).Key,
                _ingredients.IceOptions.ElementAt(Random.Range(0, _ingredients.IceOptions.Count)).Key,
                _ingredients.SugarOptions.ElementAt(Random.Range(0, _ingredients.SugarOptions.Count)).Key,
                _ingredients.ExtraToppingOptions.ElementAt(Random.Range(0, _ingredients.ExtraToppingOptions.Count)).Key);
        }

        private static ILevelIngredients GetIngredients()
        {
            switch (Level.Instance.LevelIndex)
            {
                case 1:
                    return new Level1Ingredients();
                case 2:
                    return new Level2Ingredients();
                case 3:
                    return new Level3Ingredients();
                default:
                    return new Level1Ingredients();
            }
        }
        
        public static string GetBobaText(int index) => _ingredients.BobaOptions[index];

        public static string GetIceText(int index) => _ingredients.IceOptions[index];

        public static string GetSugarText(int index) => _ingredients.SugarOptions[index];

        public static string GetExtraToppingText(int index) => _ingredients.ExtraToppingOptions[index];
    }
}
