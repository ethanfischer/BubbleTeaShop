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

            if (NeedsTutorialOrder())
            {
                return HandleTutorialOrder();
            }
            
            return new Order(
                _ingredients.BobaOptions.ElementAt(Random.Range(0, _ingredients.BobaOptions.Count)).Key,
                _ingredients.IceOptions.ElementAt(Random.Range(0, _ingredients.IceOptions.Count)).Key,
                _ingredients.SugarOptions.ElementAt(Random.Range(0, _ingredients.SugarOptions.Count)).Key,
                _ingredients.TeaOptions.ElementAt(Random.Range(0, _ingredients.TeaOptions.Count)).Key,
                _ingredients.ExtraToppingOptions.ElementAt(Random.Range(0, _ingredients.ExtraToppingOptions.Count)).Key);
        }
        static bool NeedsTutorialOrder()
        {
            return !Tutorial.Instance.CompletedTutorials[Level.Instance.LevelIndex];
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
                case 4:
                    return new Level4Ingredients();
                case 5:
                    return new Level5Ingredients();
                default:
                    return new Level5Ingredients();
            }
        }

        static Order HandleTutorialOrder()
        {
            switch (Level.Instance.LevelIndex)
            {
                case 1:
                    return new Order(1, 1, 1, 0, 0);
                case 2:
                    return new Order(1, 2, 2, 0, 0);
                case 3:
                    return new Order(1, 2, 2, 0, 1);
                case 4:
                    return new Order(3, 2, 2, 0, 1);
                case 5:
                    return new Order(3, 2, 2, 0, 1);
                default:
                    return new Order(1, 1, 1, 0, 0);
            }
        }

        public static string GetBobaText(int index) => _ingredients.BobaOptions[index];

        public static string GetIceText(int index) => _ingredients.IceOptions[index];

        public static string GetSugarText(int index) => _ingredients.SugarOptions[index];
        public static string GetTeaText(int index) => _ingredients.TeaOptions[index];

        public static string GetExtraToppingText(int index) => _ingredients.ExtraToppingOptions[index];
    }
}
