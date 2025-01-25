namespace DefaultNamespace
{
    public class Order
    {
        public Order(int boba, int ice, int sugar, int extraTopping)
        {
            Boba = boba;
            Ice = ice;
            //Milk
            Sugar = sugar;
            //Tea
            ExtraTopping = extraTopping;
        }
        
        public int Boba { get; private set; }
        public int Ice { get; private set; }
        public int Sugar { get; private set; }
        public int ExtraTopping { get; private set; }
    }
}
