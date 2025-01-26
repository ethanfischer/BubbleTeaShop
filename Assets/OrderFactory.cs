using UnityEngine;
namespace DefaultNamespace
{
    public static class OrderFactory
    {
        static string[] _sizeOptions = new string[]
        {
            "Small",
            "Medium",
            "Large"
        };

        static string[] _bobaOptions = new string[]
        {
            "-",
            "Boba",
            "Jelly"
        };

        static string[] _sugarOptions = new string[]
        {
            "-",
            "Less Sugar",
            "Regular Sugar",
            "Extra Sugar",
        };

        static string[] _iceOptions = new string[]
        {
            "-",
            "Less Ice",
            "Regular Ice",
            "Extra Ice"
        };

        static string[] _extraToppingOptions = new string[]
        {
            "-",
            "Cheese Foam",
        };
        
        public static Order CreateOrder()
        {
            return new Order(
                Random.Range(0, _bobaOptions.Length),
                Random.Range(0, _iceOptions.Length),
                Random.Range(0, _sugarOptions.Length),
                new[] { 0, 0, 1 }[Random.Range(0, 2)]);
        }
        
        public static string GetBobaText(int index) => _bobaOptions[index];

        public static string GetIceText(int index) => _iceOptions[index];

        public static string GetSugarText(int index) => _sugarOptions[index];

        public static string GetExtraToppingText(int index) => _extraToppingOptions[index];
    }
}
