namespace DefaultNamespace
{
    public class Order
    {
        public Order(int boba, int ice, int sugar, int tea, int extraTopping)
        {
            Boba = boba;
            Ice = ice;
            //Milk
            Sugar = sugar;
            Tea = tea;
            ExtraTopping = extraTopping;
        }
        public int Tea { get; private set; }
        public int Boba { get; private set; }
        public int Ice { get; private set; }
        public int Sugar { get; private set; }
        public int ExtraTopping { get; private set; }
    }
}
